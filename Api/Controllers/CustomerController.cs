using System.Collections.Generic;
using System.Net;
using Api.ViewModels;
using Business.Customer;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Customer;

namespace Api.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedListResponse<CustomerViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] PagedListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = GetCustomers();

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp.ErrorCode);
            }

            return Ok(resp);
        }

        private ApiResponse<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            var apiResp = new ApiResponse<IEnumerable<CustomerViewModel>>
            {
                Type = ResponseType.Fail
            };

            //var resp = _customerBusiness.SearchPosts("search text");

            //apiResp.Data = resp.Select(p => new PostViewModel
            //{
            //    Id = p.Id,
            //    Title = p.Title
            //});

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

    }
}