using System;
using System.Linq;
using System.Net;
using Api.ViewModels.Customer;
using Business.Customer;
using Common;
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("customers"), ApiController, Authorize]
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedListResponse<CustomerViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] SearchCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Search(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp.ErrorCode);
            }

            return Ok(resp);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody] CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = Add(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp.ErrorCode);
            }

            return Ok(resp);
        }

        private ApiResponse<PagedListResponse<CustomerViewModel>> Search(SearchCustomerViewModel model)
        {
            var apiResp = new ApiResponse<PagedListResponse<CustomerViewModel>>
            {
                Type = ResponseType.Fail,
                Data = new PagedListResponse<CustomerViewModel>()
            };

            var request = new FilteredPagedListRequest<SearchCustomerCriteria>
            {
                FilterCriteria = new SearchCustomerCriteria
                {
                    AuthorizedPersonName = model.AuthorizedPersonName,
                    Title = model.Title,
                    UserId = GetUserId().Value
                },
                IncludeRecordsTotal = model.IncludeRecordsTotal,
                Limit = model.Limit,
                Offset = model.Offset
            };

            var resp = _customerBusiness.Search(request);

            apiResp.Data.Items = resp.Items.Select(p => new CustomerViewModel
            {
                Id = p.Id,
                Title = p.Title,
                AuthorizedPersonName = p.AuthorizedPersonName,
                CreatedAtText = p.CreatedAt.ToDateString(),
                PhoneNumber = p.PhoneNumber,
                RemainingBalance = p.RemainingBalance
            });

            apiResp.Data.RecordsTotal = resp.RecordsTotal;
            apiResp.Type = ResponseType.Success;

            return apiResp;
        }

        private ApiResponse Add(CreateCustomerViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var customer = new Dto.Customer
            {
                AuthorizedPersonName = model.AuthorizedPersonName,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                UserId = GetUserId().Value,
                CreatedAt = DateTime.UtcNow.ToTurkeyDateTime()
            };

            var resp = _customerBusiness.Add(customer);

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