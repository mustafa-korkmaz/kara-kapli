using System;
using System.Net.Http;
using Common;
using Common.Response;
using Dal.Repositories.File;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service.File
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IFileRepository _repository;

        public FileService(IFileRepository repository, IOptions<AppSettings> appSettings, ILogger<FileService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
            _repository = repository;
        }

        public DataResponse<Dto.File> Get(string fileName)
        {
            var resp = new DataResponse<Dto.File>
            {
                Type = ResponseType.RecordNotFound
            };

            var id = fileName.Split("_")[0];

            var file = _repository.GetById(id);


            if (file == null || file.OwnerId != OwnerId.ToString())
            {
                return resp;
            }

            resp.Data = new Dto.File
            {
                Content = file.Content,
                Name = file.Name
            };

            resp.Type = ResponseType.Success;

            return resp;
        }

        public Guid OwnerId { get; set; }

        public void Save(Dto.File file)
        {
            var zipFileName = file.Name;

            //keep name length as max 70
            if (zipFileName.Length > 65)
            {
                zipFileName = $"{zipFileName.Substring(0, 66)}.zip";
            }
            else
            {
                zipFileName += ".zip";
            }

            var entity = new Dal.Entities.File
            {
                Name = zipFileName,
                OwnerId = file.OwnerId,
                Content = file.Content
            };

            _repository.Insert(entity);

            file.Id = entity.Id;
            file.Name = zipFileName;

            _logger.LogInformation($"{zipFileName} saved successfully");
        }

        public bool ValidateSize(long size)
        {
            return _appSettings.Value.Upload.MaxSizeInBytes >= size;
        }

    }
}