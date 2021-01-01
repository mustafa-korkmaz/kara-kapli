using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Security;
using System.Collections.Generic;
using Business.User;

namespace Api.Configurations.Jwt
{
    public static class JwtConfiguration
    {
        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    "Firebase");
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //add api auth and firebase auth
            var appId = configuration["Keys:FirebaseProjectId"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = JwtTokenConstants.IssuerSigningKey,
                    ValidAudience = JwtTokenConstants.Audience,
                    ValidIssuer = JwtTokenConstants.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            })
            .AddJwtBearer("Firebase", options =>
             {
                 options.Authority = $"https://securetoken.google.com/{appId}";
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = $"https://securetoken.google.com/{appId}",
                     ValidateAudience = true,
                     ValidAudience = appId,
                     ValidateLifetime = true,
                 };
                 options.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = async ctx =>
                     {
                         await AddUserIdClaim(ctx.Principal, ctx.HttpContext);
                     },
                 };
             });
        }

        private static async Task AddUserIdClaim(ClaimsPrincipal user, HttpContext ctx)
        {
            var service = ctx.RequestServices.GetService<ISecurity>();

            var email = user.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            var userId = await service.GetUserId(email);

            if (userId == null)
            {
                userId = Guid.NewGuid();

                var newUser = GetNewUser(userId.Value, user, ctx);

                await service.Register(newUser, "");
            }

            var id = new ClaimsIdentity();
            id.AddClaim(new Claim("id", userId.Value.ToString()));

            user.AddIdentity(id);
        }

        private static Dto.User.ApplicationUser GetNewUser(Guid newUserId, ClaimsPrincipal principal, HttpContext ctx)
        {
            var now = DateTime.UtcNow;
            var expirationDate = now.AddDays(15);

            var userBusiness = ctx.RequestServices.GetService<IUserBusiness>();

            var email = principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var name = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            return new Dto.User.ApplicationUser
            {
                Id = newUserId,
                Email = email,
                NameSurname = name ?? email,
                UserName = email,
                Roles = new List<string> { DatabaseKeys.ApplicationRoleName.User },
                EmailConfirmed = true,
                Settings = userBusiness.GetDefaultSettings(),
                CreatedAt = now,
                MembershipExpiresAt = expirationDate,
                IsSocialLogin = true
            };
        }
    }
}
