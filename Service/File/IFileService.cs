
using System;
using System.Threading.Tasks;
using Common.Response;

namespace Service.File
{
    public interface IFileService
    {
        Task Save(byte[] content, string fileName);

        Task<DataResponse<byte[]>> Get(string fileName);

        Guid OwnerId { get; set; }

        bool ValidateSize(long size);
    }
}