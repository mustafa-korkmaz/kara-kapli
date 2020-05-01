using System.Collections.Generic;
using System.Linq;
using System.Net;
using Business.Customer;
using Common;
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Customer;

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
        public IActionResult Get([FromQuery] SearchCustomerRequestViewModel model)
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

        private ApiResponse<PagedListResponse<CustomerViewModel>> Search(SearchCustomerRequestViewModel model)
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

    }
}