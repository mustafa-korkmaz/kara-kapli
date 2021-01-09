using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dto;
using Business.Parameter;
using Api.ViewModels.Import.Request;
using Api.ViewModels.Parameter.Request;
using Business.Import;

namespace Api.Controllers
{
    [Route("imports"), ApiController, Authorize]
    public class ImportController : ApiControllerBase
    {
        private readonly IImportBusiness _importBusiness;

        public ImportController(IImportBusiness importBusiness)
        {
            _importBusiness = importBusiness;
        }

        [HttpPost("basic")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public IActionResult Basic([FromBody] BasicDataImportViewModel[] model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var customers = GetMappedCustomers(model);

            var resp = _importBusiness.DoBasicImport(customers.ToArray());

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("detailed")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public IActionResult Detailed([FromBody] CreateParameterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            return Ok();
        }

        private IEnumerable<Customer> GetMappedCustomers(BasicDataImportViewModel[] model)
        {
            var list = new List<Customer>();
            var now = DateTime.UtcNow;
            var userId = GetUserId().Value;

            foreach (var item in model)
            {
                var c = new Customer
                {
                    AuthorizedPersonName = item.Customer.AuthorizedPersonName,
                    Title = item.Customer.Title,
                    UserId = userId,
                    ReceivableBalance = item.ReceivableBalance.Value,
                    DebtBalance = item.DebtBalance.Value,
                    CreatedAt = now,
                    Transactions = new List<Transaction>()
                };

                if (item.DebtBalance.Value > 0)
                {
                    c.Transactions.Add(new Transaction
                    {
                        TypeId = DatabaseKeys.ParameterId.SystemDebt,
                        Amount = item.DebtBalance.Value,
                        IsDebt = true,
                        CreatedAt = now,
                        Date = now.Date
                    });
                }

                if (item.ReceivableBalance.Value > 0)
                {
                    c.Transactions.Add(new Transaction
                    {
                        TypeId = DatabaseKeys.ParameterId.SystemReceivable,
                        Amount = item.ReceivableBalance.Value,
                        IsDebt = false,
                        CreatedAt = now,
                        Date = now.Date
                    });
                }

                list.Add(c);
            }

            return list;
        }
    }
}