using System.Net;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dto;
using Business.Parameter;
using Api.ViewModels.Import.Request;
using Api.ViewModels.Parameter.Request;

namespace Api.Controllers
{
    [Route("imports"), ApiController, Authorize]
    public class ImportController : ApiControllerBase
    {
        private readonly IParameterBusiness _parameterBusiness;

        public ImportController(IParameterBusiness parameterBusiness)
        {
            _parameterBusiness = parameterBusiness;
        }

        [HttpPost("basic")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public IActionResult Basic([FromBody] BasicDataImportViewModel[] model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            return Ok(12);

            //var resp = Create(model);

            //if (resp.Type != ResponseType.Success)
            //{
            //    return BadRequest(resp);
            //}

            //return Ok(resp);
        }

        [HttpPost("detailed")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Detailed([FromBody] CreateParameterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Create(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        private ApiResponse Create(CreateParameterViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };
            var parameter = new Parameter
            {
                UserId = GetUserId().Value,
                Name = model.Name,
                Order = model.Order.Value,
                ParameterTypeId = model.ParameterTypeId.Value
            };

            _parameterBusiness.OwnerId = parameter.UserId;

            var resp = _parameterBusiness.Add(parameter);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse Update(int id, UpdateParameterViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var parameter = new Parameter
            {
                Id = id,
                UserId = GetUserId().Value,
                Name = model.Name,
                Order = model.Order.Value,
                ParameterTypeId = model.ParameterTypeId.Value
            };

            _parameterBusiness.OwnerId = parameter.UserId;

            var resp = _parameterBusiness.Edit(parameter);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse Delete(int parameterId)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            _parameterBusiness.OwnerId = GetUserId().Value;

            var resp = _parameterBusiness.SoftDelete(parameterId);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }
    }
}