using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ViewModels.User;
using Api.ViewModels.User.Request;
using Api.ViewModels.User.Response;
using Business.User;
using Common;
using Common.Response;
using Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security;

namespace Api.Controllers
{
    [Route("account"), ApiController, Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly ISecurity _security;
        private readonly IUserBusiness _userBusiness;

        public AccountController(ISecurity security, IUserBusiness userBusiness)
        {
            _security = security;
            _userBusiness = userBusiness;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody]GetTokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await GetTokenResponse(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await RegisterUser(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("demo")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterDemo([FromBody]RegisterDemoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await RegisterDemoUser(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resp = await GetUser();

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        private async Task<ApiResponse<TokenViewModel>> GetTokenResponse(GetTokenViewModel model)
        {
            var apiResp = new ApiResponse<TokenViewModel>
            {
                Type = ResponseType.Fail
            };

            var applicationUser = new ApplicationUser
            {
                Email = model.EmailOrUsername,
                UserName = model.EmailOrUsername
            };

            var securityResp = await _security.GetToken(applicationUser, model.Password);

            if (securityResp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = securityResp.ErrorCode;
                return apiResp;
            }

            var viewModel = new TokenViewModel
            {
                Username = applicationUser.UserName,
                AccessToken = securityResp.Data,
                Email = applicationUser.Email,
                NameSurname = applicationUser.NameSurname,
                Id = applicationUser.Id.ToString()
            };

            apiResp.Data = viewModel;
            apiResp.Type = ResponseType.Success;

            return apiResp;
        }

        /// <summary>
        /// returns user info by identity manager
        /// </summary>
        /// <returns></returns>
        private async Task<ApiResponse<UserViewModel>> GetUser()
        {
            var user = await _security.GetUser(User);

            return new ApiResponse<UserViewModel>
            {
                Type = ResponseType.Success,
                Data = new UserViewModel
                {
                    Id = user.Id.ToString(),
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    NameSurname = user.NameSurname,
                    Username = user.UserName,
                    EmailConfirmed = user.EmailConfirmed
                }
            };
        }

        /// <summary>
        /// creates new user
        /// </summary>
        /// <param name="model"></param>
        private async Task<ApiResponse> RegisterUser(RegisterViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                NameSurname = model.NameSurname,
                UserName = model.Username,
                Roles = new List<string> { DatabaseKeys.ApplicationRoleName.User },
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var registerResp = await _security.Register(applicationUser, model.Password);

            if (registerResp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = registerResp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        /// <summary>
        /// creates new user
        /// </summary>
        /// <param name="model"></param>
        private async Task<ApiResponse<string>> RegisterDemoUser(RegisterDemoViewModel model)
        {
            var apiResp = new ApiResponse<string>
            {
                Type = ResponseType.Fail
            };

            var id = Guid.NewGuid();
            var username = id.ToString("N");

            var applicationUser = new ApplicationUser
            {
                Id = id,
                Email = username + "@d.com",
                NameSurname = model.Language == Language.Turkish ? "Demo A.Ş." : "Demo Corp.",
                UserName = username,
                EmailConfirmed = true,
                Roles = new List<string> { DatabaseKeys.ApplicationRoleName.DemoUser },
                CreatedAt = DateTime.UtcNow
            };

            var resp = await _security.Register(applicationUser, model.Password);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            resp = _userBusiness.CreateDemoUserEntries(applicationUser.Id, model.Language);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Data = applicationUser.Email;
            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

    }
}