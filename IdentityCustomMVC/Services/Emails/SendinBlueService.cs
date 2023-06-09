using IdentityCustomMVC.Interfaces.Emails;
using IdentityCustomMVC.Settings.Emails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IdentityCustomMVC.Services.Emails
{
    public class SendinBlueService : IEmailService
    {
        private readonly SendinBlueSettings _sendinBlueSettings;

        public SendinBlueService(IOptions<SendinBlueSettings> sendingBlueSettings)
        {
            _sendinBlueSettings = sendingBlueSettings.Value;
        }

        public async Task SendEmailAsync(string senderEmail, string subject,
                                   string messageText, string messageHtml)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_sendinBlueSettings.SenderName,
                _sendinBlueSettings.SenderEmail));

            message.To.Add(MailboxAddress.Parse(senderEmail));

            message.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = messageText,
                HtmlBody = messageHtml,
            };

            message.Body = builder.ToMessageBody();

            try
            {
                var smtpClient = new SmtpClient();

                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtpClient.ConnectAsync(_sendinBlueSettings.ServerAddress,
                                              _sendinBlueSettings.ServerPort).ConfigureAwait(false);

                await smtpClient.AuthenticateAsync(_sendinBlueSettings.SenderEmail,
                                                   _sendinBlueSettings.Password).ConfigureAwait(false);

                await smtpClient.SendAsync(message).ConfigureAwait(false);

                await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
