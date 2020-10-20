using System.Net;
using Api.ViewModels.Customer.Request;
using Business.Feedback;
using Common;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("feedbacks"), ApiController, Authorize]
    public class FeedbackController : ApiControllerBase
    {
        private readonly IFeedbackBusiness _feedbackBusiness;

        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            _feedbackBusiness = feedbackBusiness;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public IActionResult Post([FromBody] FeedbackViewModel model)
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

        private ApiResponse<int> Create(FeedbackViewModel model)
        {
            var apiResp = new ApiResponse<int>
            {
                Type = ResponseType.Fail
            };

            var feedback = new Dto.Feedback
            {
                Email = model.Email,
                Name = model.Name,
                Message = model.Message
            };

            var resp = _feedbackBusiness.Add(feedback);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            apiResp.Data = feedback.Id;

            return apiResp;
        }

    }
}