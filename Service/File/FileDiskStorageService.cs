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
    public class FileDiskStorageService : IFileService
    {
        private readonly ILogger<FileDiskStorageService> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private static readonly HttpClient Client = new HttpClient();
        private readonly string _apiUrl;

        public FileDiskStorageService(IOptions<AppSettings> appSettings, ILogger<FileDiskStorageService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
            _apiUrl = _appSettings.Value.Upload.ApiUrl;
        }

        public async Task<DataResponse<Dto.File>> Get(string fileName)
        {
            var resp = new DataResponse<Dto.File>
            {
                Type = ResponseType.RecordNotFound
            };

            var url = $"{_apiUrl}/v1/uploads/{OwnerId}%2F{fileName}";

            var result = await Client.GetAsync(url);

            var jsonString = await result.Content.ReadAsStringAsync();

            result.EnsureSuccessStatusCode();

            var content = JsonConvert.DeserializeObject<FileResponse>(jsonString);

            if (content.error_code == "RECORD_NOT_FOUND")
            {
                resp.ErrorCode = ErrorCode.RecordNotFound;
                return resp;
            }

            resp.Data = new Dto.File
            {
                Content = content.data,
                Name = fileName
            };
            resp.Type = ResponseType.Success;

            return resp;
        }

        public Guid OwnerId { get; set; }

        public async Task Save(Dto.File file)
        {
            var fileId = Guid.NewGuid().ToString("N");

            var zipFileName = $"{fileId}_{file.Name}";

            //keep name length as max 70
            if (zipFileName.Length > 65)
            {
                zipFileName = $"{zipFileName.Substring(0, 66)}.zip";
            }
            else
            {
                zipFileName += ".zip";
            }

            var payload = new
            {
                content = file.Content,
                name = $"{file.OwnerId}/{zipFileName}"
            };

            var json = JsonConvert.SerializeObject(payload);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await Client.PostAsync($"{_apiUrl}/v1/uploads", data);

            result.EnsureSuccessStatusCode();

            file.Id = fileId;
            file.Name = zipFileName;

            _logger.LogInformation($"{zipFileName} saved successfully");
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