using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        [HttpPost("reset")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Reset([FromBody]ResetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await ResetAccount(model.EmailOrUsername);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await ResetUserPassword(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }


        [HttpPost("password")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Password([FromBody]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await ChangePassword(model.Password);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("token")]
        [ProducesResponseType(typeof(ApiResponse<TokenViewModel>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody]GetTokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = await GetToken(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(ApiResponse<TokenViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult Update([FromBody]UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = UpdateUser(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var resp = await GetUser();

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [HttpPost("settings")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public IActionResult Settings([FromBody]SettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelStateErrorResponse(ModelState));
            }

            var resp = UpdateSettings(model);

            if (resp.Type != ResponseType.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        private async Task<ApiResponse<TokenViewModel>> GetToken(GetTokenViewModel model)
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
                Id = applicationUser.Id.ToString(),
                Username = applicationUser.UserName,
                AccessToken = securityResp.Data,
                Email = applicationUser.Email,
                NameSurname = applicationUser.NameSurname
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

            var settings = _userBusiness.GetSettings(user.Settings);

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
                    EmailConfirmed = user.EmailConfirmed,
                    Title = user.Title,
                    Roles = user.Roles,
                    MembershipExpiresAt = user.MembershipExpiresAt,
                    Settings = new Settings
                    {
                        FixedHeader = settings.FixedHeader,
                        OpenTagsView = settings.OpenTagsView,
                        ThemeColor = settings.ThemeColor,
                        PaginationAlign = settings.PaginationAlign
                    }
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

            var now = DateTime.UtcNow;
            var expirationDate = now.AddDays(15);

            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                NameSurname = model.NameSurname,
                UserName = model.Email,
                Roles = new List<string> { DatabaseKeys.ApplicationRoleName.User },
                EmailConfirmed = true,
                Settings = _userBusiness.GetDefaultSettings(),
                CreatedAt = now,
                MembershipExpiresAt = expirationDate
            };

            var resp = await _security.Register(applicationUser, model.Password);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            resp = _userBusiness.CreateUserDefaultEntries(applicationUser.Id, model.Language);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
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
            var now = DateTime.UtcNow;
            var expirationDate = now.AddDays(15);

            var applicationUser = new ApplicationUser
            {
                Id = id,
                Email = username + "@d.com",
                Title = model.Language == Language.Turkish ? "Demo A.Ş." : "Demo Corp.",
                UserName = username,
                EmailConfirmed = true,
                Roles = new List<string> { DatabaseKeys.ApplicationRoleName.DemoUser },
                Settings = _userBusiness.GetDefaultSettings(),
                CreatedAt = now,
                MembershipExpiresAt = expirationDate
            };

            var resp = await _security.Register(applicationUser, model.Password);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            resp = _userBusiness.CreateDemoUserDefaultEntries(applicationUser.Id, model.Language);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Data = applicationUser.Email;
            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private async Task<ApiResponse> ResetAccount(string emailOrUsername)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var resp = await _security.ResetAccount(emailOrUsername);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private async Task<ApiResponse> ResetUserPassword(ResetPasswordViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var confirmResp = await _security.ConfirmPasswordReset(model.Password, model.SecurityCode);

            if (confirmResp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = confirmResp.ErrorCode;
                return apiResp;
            }

            var resp = await _security.ChangePassword(confirmResp.Data, model.Password);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse UpdateSettings(SettingsViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var userSettings = new UserSettings
            {
                OpenTagsView = model.OpenTagsView.Value,
                FixedHeader = model.FixedHeader.Value,
                ThemeColor = model.ThemeColor,
                PaginationAlign = model.PaginationAlign
            };

            var resp = _userBusiness.UpdateSettings(GetUserId().Value, userSettings);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private async Task<ApiResponse> ChangePassword(string password)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var resp = await _security.ChangePassword(GetUserId().Value, password);

            if (resp.Type != ResponseType.Success)
            {
                apiResp.ErrorCode = resp.ErrorCode;
                return apiResp;
            }

            apiResp.Type = ResponseType.Success;
            return apiResp;
        }

        private ApiResponse UpdateUser(UpdateUserViewModel model)
        {
            var apiResp = new ApiResponse
            {
                Type = ResponseType.Fail
            };

            var resp = _userBusiness.UpdateCompanyInformation(GetUserId().Value, model.Title, model.AuthorizedPersonName);

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