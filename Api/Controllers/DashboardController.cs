using System.Net;
using Api.ViewModels.Dashboard.Response;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Dashboard;

namespace Api.Controllers
{
    [Route("dashboard"), ApiController, Authorize]
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardBusiness _dashboardBusiness;

        public DashboardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserDashboardViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var resp = GetDashboard();

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        private ApiResponse<UserDashboardViewModel> GetDashboard()
        {
            var apiResp = new ApiResponse<UserDashboardViewModel>
            {
                Type = ResponseType.Fail,
            };

            var resp = _dashboardBusiness.GetUserDashboard(GetUserId().Value);

            var data = new UserDashboardViewModel
            {
                TransactionCount = resp.TransactionCount,
                CustomerCount = resp.Customers.Count,
                CustomerDebtsTotal = resp.CustomerDebtsTotal,
                CustomerReceivablesTotal = resp.CustomerReceivablesTotal
            };

            apiResp.Data = data;
            apiResp.Type = ResponseType.Success;

            return apiResp;
        }
    }
}