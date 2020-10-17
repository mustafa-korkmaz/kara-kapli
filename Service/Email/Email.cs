using System.Collections.Generic;

namespace Service.Email
{
    public class Email
    {
        /// <summary>
        /// Sender's mail address
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Display name for mail sender
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// MailTo text. Use ';' char between addresses for more than one mail sending operations.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// To option when a mail wanted to be replied.
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// mail sender's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// sets subject of mail
        /// </summary>
        public string Subject { get; set; }

        public Template Template { get; set; }

        public Email(string from, string password)
        {
            From = from;
            Password = password;
        }

        public Email()
        {
        }
    }

    /// <summary>
    /// mail environment settings
    /// </summary>
    public class Settings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string TemplatePath { get; set; }
    }

    public class Template
    {
        /// <summary>
        /// template name. eg: myEmailTemplate.html
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// template variables to replace words
        /// </summary>
        public Dictionary<string, string> Variables { get; set; }
    }
}
