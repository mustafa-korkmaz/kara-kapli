using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly Settings _emailSettings;
        private readonly ILogger<EmailService> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public EmailService(IOptions<AppSettings> appSettings, ILogger<EmailService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;

            _emailSettings = new Settings
            {
                Port = appSettings.Value.Email.Port,
                UseSsl = appSettings.Value.Email.UseSsl,
                Host = appSettings.Value.Email.Host,
                TemplatePath = appSettings.Value.Email.TemplatePath
            };
        }

        public bool SendEmail(Email email)
        {
            if (string.IsNullOrEmpty(email.From))
            {
                email.From = _appSettings.Value.Email.MailFrom;
                email.DisplayName = _appSettings.Value.Email.MailDisplayName;
                email.Password = _appSettings.Value.Email.MailPassword;
            }

            if (!email.Template.Variables.ContainsKey("admin_panel_url"))
            {
                email.Template.Variables.Add("admin_panel_url", _appSettings.Value.AdminPanelUrl);
            }

            if (!email.Template.Variables.ContainsKey("landing_page_url"))
            {
                email.Template.Variables.Add("landing_page_url", _appSettings.Value.LandingPageUrl);
            }

            var mailHandler = MailHandler.Instance;

            mailHandler.Settings = _emailSettings;

            mailHandler.Send(email);

            if (mailHandler.HasError)
            {
                _logger.LogError($"sending to {email.To} failed.", mailHandler.GetException());
            }

            return !mailHandler.HasError;
        }
    }
}