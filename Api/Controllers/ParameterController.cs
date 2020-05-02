using System.Linq;
using System.Net;
using Api.ViewModels.Parameter;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.Request;
using Dto;
using Business.Parameter;
using Common.Request.Criteria.Parameter;
using Api.ViewModels;

namespace Api.Controllers
{
    [Route("parameters"), ApiController, Authorize]
    public class ParameterController : ApiControllerBase
    {
        private readonly IParameterBusiness _parameterBusiness;

        public ParameterController(IParameterBusiness parameterBusiness)
        {
            _parameterBusiness = parameterBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedListResponse<ParameterViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] SearchParameterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Search(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody] CreateParameterViewModel model)
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Put([FromRoute] IdViewModel idModel, [FromBody] UpdateParameterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Update(idModel.Id, model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Delete([FromRoute] IdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Delete(model.Id);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        private ApiResponse<PagedListResponse<ParameterViewModel>> Search(SearchParameterViewModel model)
        {
            var apiResp = new ApiResponse<PagedListResponse<ParameterViewModel>>
            {
                Type = ResponseType.Fail,
                Data = new PagedListResponse<ParameterViewModel>()
            };

            var request = new FilteredPagedListRequest<SearchParameterCriteria>
            {
                FilterCriteria = new SearchParameterCriteria
                {
                    UserId = GetUserId().Value,
                    Name = model.Name
                },
                IncludeRecordsTotal = model.IncludeRecordsTotal,
                Limit = model.Limit,
                Offset = model.Offset
            };

            var resp = _parameterBusiness.Search(request);

            apiResp.Data.Items = resp.Items.Select(p => new ParameterViewModel
            {
                Id = p.Id,
                ParameterTypeId = p.ParameterTypeId,
                Order = p.Order,
                Name = p.Name
            });

            apiResp.Data.RecordsTotal = resp.RecordsTotal;
            apiResp.Type = ResponseType.Success;

            return apiResp;
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
                Id =id,
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