
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{


    public class EmailService : IEmailService
    {
        private readonly ISmtpSettings _smtpData;

        public EmailService(ISmtpSettings smtpData)
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

        public string GetSecret() => _smtpData.Secret;
    }
}
