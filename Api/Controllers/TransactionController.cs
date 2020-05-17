using System;
using System.Linq;
using System.Net;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.Request.Criteria.Transaction;
using Common.Request;
using Business.Transaction;
using Api.ViewModels;
using Api.ViewModels.Customer.Response;
using Api.ViewModels.Parameter.Response;
using Api.ViewModels.Transaction.Request;
using Api.ViewModels.Transaction.Response;

namespace Api.Controllers
{
    [Route("transactions"), ApiController, Authorize]
    public class TransactionController : ApiControllerBase
    {
        private readonly ITransactionBusiness _transactionBusiness;

        public TransactionController(ITransactionBusiness transactionBusiness)
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Put([FromRoute] IdViewModel idModel, [FromBody] UpdateTransactionViewModel model)
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
                    IsDebt = model.IsDebt
                },
                IncludeRecordsTotal = model.IncludeRecordsTotal,
                Limit = model.Limit,
                Offset = model.Offset
            };

            _transactionBusiness.OwnerId = GetUserId().Value;

            var resp = _transactionBusiness.Search(request);

            apiResp.Data.Items = resp.Items.Select(p => new TransactionViewModel
            {
                Id = p.Id,
                Customer = new CustomerViewModel
                {
                    Id = p.CustomerId,
                    Title = p.Customer.Title,
                    AuthorizedPersonName = p.Customer.AuthorizedPersonName
                },
                Type = new ParameterViewModel
                {
                    Id = p.Type.Id,
                    ParameterTypeId = p.Type.ParameterTypeId,
                    Name = p.TypePrettyName
                },
                Amount = p.Amount,
                Description = p.Description,
                IsDebt = p.IsDebt,
                CreatedAtText = p.CreatedAt.ToDateTimeString(),
                ModifiedAtText = p.ModifiedAt.ToDateTimeString(),
                DateText = p.Date.ToDateString(),
            }); ;

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

            _transactionBusiness.OwnerId = GetUserId().Value;

            var now = DateTime.UtcNow.ToTurkeyDateTime();

            var transaction = new Dto.Transaction
            {
                CustomerId = model.CustomerId,
                TypeId = model.TypeId.Value,
                Amount = model.Amount.Value,
                Description = model.Description,
                IsDebt = _transactionBusiness.IsDebtTransaction(model.TypeId.Value),
                Date = model.Date,
                ModifiedAt = now,
                CreatedAt = now
            };

            var resp = _transactionBusiness.Add(transaction);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse Update(int id, UpdateTransactionViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var transaction = new Dto.Transaction
            {
                Id = id,
                TypeId = model.TypeId.Value,
                Amount = model.Amount.Value,
                Description = model.Description,
                IsDebt = _transactionBusiness.IsDebtTransaction(model.TypeId.Value),
                Date = model.Date,
                ModifiedAt = DateTime.UtcNow.ToTurkeyDateTime()
            };

            _transactionBusiness.OwnerId = GetUserId().Value;

            var resp = _transactionBusiness.Edit(transaction);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse Delete(int transactionId)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            _transactionBusiness.OwnerId = GetUserId().Value;

            var resp = _transactionBusiness.Delete(transactionId);

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