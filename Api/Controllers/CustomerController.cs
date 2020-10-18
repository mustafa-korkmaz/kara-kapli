using System;
using System.Linq;
using System.Net;
using System.Threading;
using Api.ViewModels;
using Api.ViewModels.Customer.Request;
using Api.ViewModels.Customer.Response;
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
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody] CreateCustomerViewModel model)
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
        public IActionResult Put([FromRoute] IdViewModel idModel, [FromBody] UpdateCustomerViewModel model)
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
                    UserId = GetUserId().Value,
                    SortType = model.GetSortType()
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
                CreatedAt = p.CreatedAt,
                PhoneNumber = p.PhoneNumber,
                DebtBalance = p.DebtBalance,
                ReceivableBalance = p.ReceivableBalance
            });

            apiResp.Data.RecordsTotal = resp.RecordsTotal;
            apiResp.Type = ResponseType.Success;

            return apiResp;
        }

        private ApiResponse<int> Create(CreateCustomerViewModel model)
        {
            var apiResp = new ApiResponse<int>
            {
                Type = ResponseType.Fail
            };

            var customer = new Dto.Customer
            {
                AuthorizedPersonName = model.AuthorizedPersonName,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                UserId = GetUserId().Value,
                CreatedAt = DateTime.UtcNow
            };

            var resp = _customerBusiness.Add(customer);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            apiResp.Data = customer.Id;

            return apiResp;
        }

        private ApiResponse Update(int id, UpdateCustomerViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var customer = new Dto.Customer
            {
                Id = id,
                AuthorizedPersonName = model.AuthorizedPersonName,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                UserId = GetUserId().Value
            };

            _customerBusiness.OwnerId = customer.UserId;

            var resp = _customerBusiness.Edit(customer);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse Delete(int customerId)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            _customerBusiness.OwnerId = GetUserId().Value;

            var resp = _customerBusiness.Delete(customerId);

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