using System;
using System.Linq;
using System.Net;
using Api.ViewModels.Customer;
using Api.ViewModels.CustomerOperation;
using Api.ViewModels.Parameter;
using Business.Customer;
using Common;
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("transactions"), ApiController, Authorize]
    public class TransactionsController : ApiControllerBase
    {
        private readonly ICustomerOperationBusiness _customerOperationBusiness;

        public TransactionsController(ICustomerOperationBusiness customerOperationBusiness)
        {
            _customerOperationBusiness = customerOperationBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedListResponse<CustomerOperationViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] SearchCustomerOperationViewModel model)
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

        //[HttpPost]
        //[ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        //public IActionResult Post([FromBody] CreateCustomerOperationRequestViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(GetModelStateErrorResponse(ModelState));
        //    }

        //    var resp = Add(model);

        //    if (resp.Type != ResponseType.Success)
        //    {
        //        return BadRequest(resp.ErrorCode);
        //    }

        //    return Ok(resp);
        //}

        private ApiResponse<PagedListResponse<CustomerOperationViewModel>> Search(SearchCustomerOperationViewModel model)
        {
            var apiResp = new ApiResponse<PagedListResponse<CustomerOperationViewModel>>
            {
                Type = ResponseType.Fail,
                Data = new PagedListResponse<CustomerOperationViewModel>()
            };

            var request = new FilteredPagedListRequest<SearchCustomerOperationCriteria>
            {
                FilterCriteria = new SearchCustomerOperationCriteria
                {
                    UserId = GetUserId().Value,
                    CustomerId = model.CustomerId,
                    IsCompleted = model.IsCompleted,
                    IsReceivable = model.IsReceivable
                },
                IncludeRecordsTotal = model.IncludeRecordsTotal,
                Limit = model.Limit,
                Offset = model.Offset
            };

            var resp = _customerOperationBusiness.Search(request);

            apiResp.Data.Items = resp.Items.Select(p => new CustomerOperationViewModel
            {
                Id = p.Id,
                Customer = new CustomerViewModel
                {
                    Title = p.Customer.Title,
                    AuthorizedPersonName = p.Customer.AuthorizedPersonName
                },
                Type = new ParameterViewModel
                {
                    Id = p.Type.Id,
                    ParameterTypeId = p.Type.ParameterTypeId,
                    Name = p.Type.Name
                },
                Amount = p.Amount,
                Description = p.Description,
                IsCompleted = p.IsCompleted,
                IsReceivable = p.IsReceivable,
                CreatedAtText = p.CreatedAt.ToDateTimeString(),
                ModifiedAtText = p.ModifiedAt.ToDateTimeString(),
                DateText = p.Date.ToDateString(),
            });

            apiResp.Data.RecordsTotal = resp.RecordsTotal;
            apiResp.Type = ResponseType.Success;

            return apiResp;
        }

        //private ApiResponse Add(CreateCustomerOperationRequestViewModel model)
        //{
        //    var apiResp = new ApiResponse
        //    {
        //        Type = ResponseType.Fail
        //    };

        //    var customer = new Dto.Customer
        //    {
        //        AuthorizedPersonName = model.AuthorizedPersonName,
        //        PhoneNumber = model.PhoneNumber,
        //        Title = model.Title,
        //        UserId = GetUserId().Value,
        //        CreatedAt = DateTime.UtcNow.ToTurkeyDateTime()
        //    };

        //    var resp = _customerOperationBusiness.Add(customer);

        //    if (resp.Type != ResponseType.Success)
        //    {
        //        apiResp.ErrorCode = resp.ErrorCode;
        //        return apiResp;
        //    }

        //    apiResp.Type = ResponseType.Success;
        //    return apiResp;
        //}
    }
}