
using System;
using System.Threading.Tasks;
using Common.Response;

namespace Service.File
{
    public interface IFileService
    {
        Task Save(Dto.File file);

        Task<DataResponse<Dto.File>> Get(string fileName);

        Guid OwnerId { get; set; }

        bool ValidateSize(long size);
    }
}