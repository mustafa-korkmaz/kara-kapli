using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Service.File
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly HttpClient _client;
        private readonly string _apiUrl;

        public FileService(IOptions<AppSettings> appSettings, ILogger<FileService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
            _apiUrl = _appSettings.Value.Upload.ApiUrl;

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _client = new HttpClient(clientHandler);
        }

        public async Task<DataResponse<byte[]>> Get(string fileName)
        {
            var resp = new DataResponse<byte[]>
            {
                Type = ResponseType.RecordNotFound
            };

            var url = $"{_apiUrl}/v1/uploads/{OwnerId}%2F{fileName}";
         
            var result = await _client.GetAsync(url);

            var jsonString = await result.Content.ReadAsStringAsync();

            result.EnsureSuccessStatusCode();

            var content = JsonConvert.DeserializeObject<FileResponse>(jsonString);

            if (content.error_code == "RECORD_NOT_FOUND")
            {
                return resp;
            }

            resp.Data = content.data;
            resp.Type = ResponseType.Success;

            _logger.LogInformation($"{fileName} downloaded successfully");

            return resp;
        }

        public Guid OwnerId { get; set; }

        public async Task Save(byte[] content, string fileName)
        {
            var payload = new
            {
                content,
                name = $"{OwnerId}/{fileName}"
            };

            var json = JsonConvert.SerializeObject(payload);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.PostAsync($"{_apiUrl}/v1/uploads", data);

            _logger.LogInformation($"{payload.name} uploaded successfully");

            result.EnsureSuccessStatusCode();
        }

        public bool ValidateSize(long size)
        {
            return _appSettings.Value.Upload.MaxSizeInBytes >= size;
        }

        struct FileResponse
        {
            public byte[] data;
            public string error_code;
        }
    }
}