using System;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{
    public class SmtpData
    {        
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpData _smtpData;

        public EmailService(SmtpData smtpData)
        {
            _smtpData = smtpData;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? _smtpData.EmailFrom);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            var smtp = new SmtpClient();
            smtp.Connect(_smtpData.SmtpHost, _smtpData.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpData.SmtpUser, _smtpData.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
