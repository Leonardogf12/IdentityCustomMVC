using IdentityCustomMVC.Interfaces.Emails;
using IdentityCustomMVC.Settings.Emails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdentityCustomMVC.Services.Emails
{
    public class SendGridService : IEmailService
    {
        private readonly SendGridSettings _sendGridSettings;

        public SendGridService(IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridSettings = sendGridSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject,
                                   string messageText, string messageHtml)
        {

            try
            {
                var sgc = new SendGridClient(_sendGridSettings.ApiKey);

                var sender = new EmailAddress(_sendGridSettings.SenderEmail,
                                                _sendGridSettings.SenderName);

                var dest = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(sender, dest, subject, messageText, messageHtml);

                await sgc.SendEmailAsync(msg);

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
