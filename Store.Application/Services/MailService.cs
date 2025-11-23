using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Store.Application.Options;
using Store.Application.Services.Interfaces;

namespace Store.Application.Services
{
    public class MailService : IMailService
    {
        private readonly GmailOptions _gmailOptions;

        public MailService(IOptions<GmailOptions> options)
        {
            _gmailOptions = options.Value;
        }

        public async Task SendEmailAsync(string recipient, string subject, string body, byte[] pdfData, CancellationToken cancellationToken)
        {
            var pdfStream = new MemoryStream(pdfData);
            var attachment = new Attachment(pdfStream, "Order.pdf", "application/pdf");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_gmailOptions.Email),
                Subject = subject,
                Body = body
            };

            mailMessage.Attachments.Add(attachment);
            mailMessage.To.Add(recipient);

            using(var smtpClient = new SmtpClient())
            {
                smtpClient.Host = _gmailOptions.Host;
                smtpClient.Port = _gmailOptions.Port;
                smtpClient.Credentials = new NetworkCredential(_gmailOptions.Email, _gmailOptions.Password);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            }
        }
    }
}
