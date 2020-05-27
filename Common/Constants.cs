using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class AppConstant
    {
        public const string ApiKeyValue = "My.ApiKey";
        public static string ApiKeyHash = Utility.GetHashValue(ApiKeyValue);
        public const int MinimumLengthForSearch = 3;
    }

    public static class AccountingType
    {
        /// <summary>
        /// ALACAK type accounting
        /// </summary>
        public const int Receivable = 1;

        /// <summary>
        /// BORCLU type accounting
        /// </summary>
        public const int Debt = 2;
    }

    public static class ErrorCode
    {
        public const string ApplicationException = "UNEXPECTED_ERROR";
        public const string RecordNotFound = "RECORD_NOT_FOUND";
        public const string NotAuthorized = "NOT_AUTHORIZED";
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string IncorrectUsernameOrPassword = "USERNAME_OR_PASSWORD_INCORRECT";
        public const string UserExists = "USER_ALREADY_EXISTS";
    }

    public static class ValidationErrorCode
    {
        public const string RequiredField = "{0}_FIELD_IS_REQUIRED";

        public const string MaxLength = "{0}_FIELD_SHOULD_BE_MAX_{1}_CHARS";

        public const string BetweenLength = "{0}_FIELD_SHOULD_BE_MIN_{2}_MAX_{1}_CHARS";

        public const string BetweenRange = "{0}_FIELD_SHOULD_BE_BETWEEN_{1}_AND_{2}";

        public const string EmailNotValid = "EMAIL_FIELD_IS_NOT_VALID";

        public const string UrlNotValid = "URL_FIELD_IS_NOT_VALID";

        public const string DateNotValid = "DATE_FORMAT_IS_NOT_VALID";
    }

    public static class LoggingOperationPhrase
    {
        public const string NewEntity = "'{0}' entity has been created.";

        public const string ModifiedEntity = "'{0}' entity has been modified.";

        public const string DeletedEntity = "'{0}' entity has been deleted.";

        public const string Registration = "New user {0} has registered.";

        public const string TokenRequest = "User {0} generated new token";

        public const string PasswordResetRequest = "User {0} has requested a password reset.";

        public const string PasswordReset = "User {0} has reset password.";

        public const string PasswordChanged = "User {0} has changed password.";

        public const string NotFound = "Object {0} not found.";
    }

    public static class ChannelType
    {
        public const string Ios = "0";
        public const string Android = "1";
        public const string WebApp = "2";
        public const string AdminPanel = "3";
    }

    #region cahce keys

    public static class CacheKey
    {
        public const string GetAllBlogs = "get_all_blogs";
    }

    #endregion cahce keys

    #region request headers

    public static class RequestHeader
    {
        public const string Authorization = "Authorization";
        public const string ApiKey = "ApiKey";
        public const string ChannelType = "ChannelType";
    }

    #endregion request headers

    #region jwt constants

    public class JwtTokenConstants
    {
        public static SecurityKey IssuerSigningKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetCryptoSecurityKey()));
            }
        }

        public static SigningCredentials SigningCredentials
        {
            get
            {
                return new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
            }
        }

        public static TimeSpan TokenExpirationTime
        {
            get
            {
                return TimeSpan.FromDays(30);
            }
        }

        public static string Issuer
        {
            get
            {
                return "Issuer";
            }
        }

        public static string Audience
        {
            get
            {
                return "Audience";
            }
        }

        private static string GetCryptoSecurityKey()
        {
            var securityKey = "19ASREAFATSUMZAMKROK07";
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(securityKey));
                return Encoding.ASCII.GetString(result);
            }
        }
    }

    #endregion 
}



