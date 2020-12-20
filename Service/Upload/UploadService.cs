using System;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service.Upload
{
    public class UploadService : IUploadService
    {
        private readonly ILogger<UploadService> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public UploadService(IOptions<AppSettings> appSettings, ILogger<UploadService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;

        }

        public async Task Save(byte[] content, string fileName)
        {
        }

        public Guid OwnerId { get; set; }

        public bool ValidateSize(long size)
        {
            return _appSettings.Value.Upload.MaxSizeInBytes >= size;
        }
    }
}