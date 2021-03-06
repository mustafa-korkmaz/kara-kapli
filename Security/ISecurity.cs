﻿using System.Security.Claims;
using Common.Response;
using Dto.User;
using System.Threading.Tasks;
using System;

namespace Security
{
    public interface ISecurity
    {
        /// <summary>
        /// Checks for user by username or email. Sets user info and returns a valid token
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<DataResponse<string>> GetToken(ApplicationUser userDto, string password);

        /// <summary>
        /// Creates user and sets user info
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Response> Register(ApplicationUser userDto, string password);

        Task<Response> ResetAccount(string emailOrUsername);

        /// <summary>
        /// validates security code and returns respective user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        Task<DataResponse<Guid>> ConfirmPasswordReset(string password, string securityCode);

        /// <summary>
        /// changes user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<Response> ChangePassword(Guid userId, string newPassword);

        /// <summary>
        /// returns current user info by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUser(string userId);

        Task<Guid?> GetUserId(string email);
    }
}
