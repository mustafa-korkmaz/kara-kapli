
namespace Common
{
    /// <summary>
    /// appsettings.json file model class
    /// </summary>
    public class AppSettings
    {
        public string ApiUrl { get; set; }

        public string PfeWebConnection { get; set; }

        public string CacheProvider { get; set; }

        /// <summary>
        /// prudential first web address
        /// </summary>
        public string PfeWebUrl { get; set; }

        /// <summary>
        /// berlin url
        /// </summary>
        public string AdminPanelUrl { get; set; }

        public LoggingSettings Logging { get; set; }

        public EMailSettings Email { get; set; }

        public SmsSettings Sms { get; set; }

        /// <summary>
        /// payment method settings
        /// </summary>
        public PaymentSettings Payment { get; set; }

        /// <summary>
        /// comma seperated reseved domains like prudentialFirst.com
        /// </summary>
        public string ReservedDomains { get; set; }
    }

    public class LoggingSettings
    {
        public bool IsActive { get; set; }

        public string InfoPath { get; set; }

        public string InfoFile { get; set; }

        public string ErrorPath { get; set; }

        public string ErrorFile { get; set; }

        public string ReqAndRespPath { get; set; }

        public string ReqAndRespFile { get; set; }
    }

    public class EMailSettings
    {
        public bool UseSsl { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public int MaximumCount { get; set; }

        public string TemplatePath { get; set; }

        public string MailFrom { get; set; }

        public string MailDisplayName { get; set; }

        public string MailPassword { get; set; }
    }

    public class SmsSettings
    {
        /// <summary>
        /// sms sending url
        /// </summary>
        public string SendSmsUrl { get; set; }

        /// <summary>
        /// value of Authorization header
        /// </summary>
        public string AuthorizationHeader { get; set; }
    }


    public class PaymentSettings
    {
        /// <summary>
        /// StripeApiKey for auth
        /// </summary>
        public string StripeApiKey { get; set; }

        /// <summary>
        /// StripePublishableKey for web requests
        /// </summary>
        public string StripePublishableKey { get; set; }

        /// <summary>
        /// Stripe web hook secret
        /// </summary>
        public string StripeWebHookSecret { get; set; }
    }


}
