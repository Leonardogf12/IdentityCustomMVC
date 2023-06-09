using IdentityCustomMVC.Interfaces.Emails;
using IdentityCustomMVC.Settings.Emails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdentityCustomMVC.Services.Emails
{
    public class GMailService : IEmailService
    {
        private readonly GMailSettings _gMailSettings;

        public GMailService(IOptions<GMailSettings> gMailSettings)
        {
            _gMailSettings = gMailSettings.Value;
        }

        public async Task SendEmailAsync(string senderEmail, string subject,
                                   string messageText, string messageHtml)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_gMailSettings.SenderName,
                    _gMailSettings.SenderEmail));

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

                    await smtpClient.ConnectAsync(_gMailSettings.ServerAddress,
                                                  _gMailSettings.ServerPort).ConfigureAwait(false);

                    await smtpClient.AuthenticateAsync(_gMailSettings.SenderEmail,
                                                       _gMailSettings.Password).ConfigureAwait(false);

                    await smtpClient.SendAsync(message).ConfigureAwait(false);

                    await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(e.Message);
                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
