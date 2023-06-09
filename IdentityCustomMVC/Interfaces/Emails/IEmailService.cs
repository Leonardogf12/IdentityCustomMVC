namespace IdentityCustomMVC.Interfaces.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string senderEmail, string subject,
                            string messageText, string messageHtml);
    }
}
