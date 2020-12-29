
using System;
using Common.Response;

namespace Service.File
{
    public interface IFileService
    {
        void Save(Dto.File file);

        DataResponse<Dto.File> Get(string fileName);

        Guid OwnerId { get; set; }

        bool ValidateSize(long size);
    }
}