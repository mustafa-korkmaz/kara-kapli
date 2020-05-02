using System;
using System.Linq;
using System.Net;
using Api.ViewModels.Customer;
using Api.ViewModels.Transaction;
using Api.ViewModels.Parameter;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.Request.Criteria.Transaction;
using Common.Request;
using Business.Transaction;

namespace Api.Controllers
{
    [Route("transactions"), ApiController, Authorize]
    public class TransactionsController : ApiControllerBase
    {
        private readonly ITransactionBusiness _transactionBusiness;

        public TransactionsController(ITransactionBusiness transactionBusiness)
        {
            _transactionBusiness = transactionBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedListResponse<TransactionViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] SearchTransactionViewModel model)
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
        public IActionResult Post([FromBody] CreateTransactionViewModel model)
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

        private ApiResponse<PagedListResponse<TransactionViewModel>> Search(SearchTransactionViewModel model)
        {
            var apiResp = new ApiResponse<PagedListResponse<TransactionViewModel>>
            {
                Type = ResponseType.Fail,
                Data = new PagedListResponse<TransactionViewModel>()
            };

            var request = new FilteredPagedListRequest<SearchTransactionCriteria>
            {
                FilterCriteria = new SearchTransactionCriteria
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

            var resp = _transactionBusiness.Search(request);

            apiResp.Data.Items = resp.Items.Select(p => new TransactionViewModel
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

        private ApiResponse Create(CreateTransactionViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var now = DateTime.UtcNow.ToTurkeyDateTime();

            var transaction = new Dto.Transaction
            {
                CustomerId = model.CustomerId,
                TypeId = model.TypeId,
                Amount = model.Amount.Value,
                Description = model.Description,
                IsCompleted = model.IsCompleted.Value,
                IsReceivable = model.IsReceivable.Value,
                Date = model.Date,
                ModifiedAt = now,
                CreatedAt = now
            };

            _transactionBusiness.OwnerId = GetUserId().Value;

            var resp = _transactionBusiness.Add(transaction);

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