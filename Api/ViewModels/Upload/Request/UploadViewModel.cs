using Microsoft.AspNetCore.Http;

namespace Api.ViewModels.Upload.Request
{
    public class UploadViewModel
    {
        public IFormFile File { get; set; }
    }
}