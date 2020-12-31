using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using Api.ViewModels.File.Request;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.File;

namespace Api.Controllers
{
    [Route("uploads"), ApiController, Authorize]
    public class UploadController : ApiControllerBase
    {
        private readonly IFileService _fileService;

        public UploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromForm] UploadFileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await Save(model);

            return Ok(resp);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            _fileService.OwnerId = GetUserId().Value;

            var resp = await _fileService.Get(name);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            const string contentType = "application/zip";
            HttpContext.Response.ContentType = contentType;

            var result = new FileContentResult(resp.Data.Content, contentType)
            {
                FileDownloadName = resp.Data.Name
            };

            return result;
        }

        private async Task<ApiResponse<string>> Save(UploadFileViewModel model)
        {
            var apiResp = new ApiResponse<string>
            {
                Type = ResponseType.Fail
            };

            if (!_fileService.ValidateSize(model.File.Length))
            {
                apiResp.ErrorCode = ErrorCode.ObjectExceededMaxAllowedLength;
                return apiResp;
            }

            var (fileName, _) = Utility.GetFileNameAndExtension(model.File.FileName);

            byte[] compressedBytes;

            await using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(model.File.FileName, CompressionLevel.Optimal);

                    await using (var entryStream = fileInArchive.Open())
                    {
                        await model.File.CopyToAsync(entryStream);
                    }
                }

                compressedBytes = outStream.ToArray();  // get compressed byte array in order to save content as .zip
            }

            var file = new Dto.File
            {
                OwnerId = GetUserId().Value.ToString(),
                Name = fileName,
                Content = compressedBytes
            };

            await _fileService.Save(file);

            apiResp.Type = ResponseType.Success;

            apiResp.Data = file.Name;

            return apiResp;
        }
    }
}