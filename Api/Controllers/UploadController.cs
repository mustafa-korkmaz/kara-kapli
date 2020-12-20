using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using Api.ViewModels.Upload.Request;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Upload;

namespace Api.Controllers
{
    [Route("uploads"), ApiController, Authorize]
    public class UploadController : ApiControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromForm] UploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await Save(model);

            return Ok(resp);
        }

        private async Task<ApiResponse<string>> Save(UploadViewModel model)
        {
            var apiResp = new ApiResponse<string>
            {
                Type = ResponseType.Fail
            };

            if (!_uploadService.ValidateSize(model.File.Length))
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

            var zipFileName = $"{Guid.NewGuid()}_{fileName}.zip";

            _uploadService.OwnerId = GetUserId().Value;

            await _uploadService.Save(compressedBytes, zipFileName);

            apiResp.Type = ResponseType.Success;
            apiResp.Data = zipFileName;

            // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{guid}_{fileName}.zip");

            // await System.IO.File.WriteAllBytesAsync(path, compressedBytes); // Requires System.Linq

            return apiResp;
        }

    }
}