using Microsoft.AspNetCore.Http;

namespace Api.ViewModels.File.Request
{
    public class UploadFileViewModel
    {
        public IFormFile File { get; set; }
    }
}