using System.Net.Mail;
using Microsoft.Extensions.Options;
using Token.BRL.Common;
using Token.BRL.Interfaces;

namespace Token.BRL.Services
{
    public class EmailService : IEmailService
    {
        private readonly Email _emailConfiguration;

        public EmailService(IOptions<Email> emailConfiguration)
        {
            this._emailConfiguration = emailConfiguration.Value;
        }

        public void SendEmail(string subject, string message, bool isHtml)
        {
            var msg = new MailMessage(
                to: _emailConfiguration.EmailTo,
                from: _emailConfiguration.EmailFrom,
                subject: subject,
                body: message
            );
            msg.IsBodyHtml = isHtml;

            using (var client = new SmtpClient())
            {
                client.Port = _emailConfiguration.SmtpPort;
                client.Host = _emailConfiguration.SmtpHost;
                //client.SendAsync(msg, new object());
                client.Send(msg);
            }
        }
    }
}