using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace Service.Email
{
    public class MailHandler
    {
        private Exception _mailException;

        public Exception GetException()
        {
            return _mailException;
        }

        public Settings Settings { set; private get; }

        public bool HasError { get; private set; }

        #region singleton definition

        public static MailHandler Instance { get; } = new MailHandler();

        private MailHandler()
        {
        }

        #endregion singleton definition


        /// <summary>
        /// Sends  mail 
        /// </summary>
        /// <returns></returns>
        public void Send(Email email)
        {
            MailMessage mail = new MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(email.From, email.Password);

            var smtpClient = new SmtpClient(Settings.Host, Settings.Port)
            {
                EnableSsl = Settings.UseSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                mail.Subject = email.Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.From = new MailAddress(email.From, email.DisplayName);
                mail.To.Add(email.To);

                if (!string.IsNullOrEmpty(email.ReplyTo))
                {
                    //this email has a special reply to address
                    mail.ReplyToList.Add(email.ReplyTo);
                }

                mail.IsBodyHtml = true;
                mail.Body = GetTemplate(email.Template.Name, email.Template.Variables);
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                smtpClient.Credentials = cred;
                smtpClient.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                HasError = true;

                _mailException = ex;
            }
        }

        /// <summary>
        /// returns mail body as html template
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="templateVariables"></param>
        /// <returns></returns>
        private string GetTemplate(string templateName, Dictionary<string, string> templateVariables)
        {
            string mailBody;

            using (StreamReader reader = new StreamReader(GetTemplatePath(templateName)))
            {
                mailBody = reader.ReadToEnd();
            }

            foreach (var variable in templateVariables)
            {
                mailBody = mailBody.Replace(variable.Key, variable.Value);
            }

            return mailBody; // set mail body as template
        }

        private string GetTemplatePath(string templateName)
        {
            var mailTemplatesPath = Settings.TemplatePath;

            var appRoot = Directory.GetCurrentDirectory();

            return appRoot + mailTemplatesPath + templateName;
        }
    }
}
