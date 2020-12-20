
using System;
using System.Threading.Tasks;

namespace Service.Upload
{
    public interface IUploadService
    {
        Task Save(byte[] content, string fileName);

        Guid OwnerId { get; set; }

        bool ValidateSize(long size);
    }
}