namespace CIPlatform.Services.Service.Interface;
public interface IEmailService
{
    void EmailSend(string email, string subject, string htmlMessage);
    void SendResetPasswordLink(string email, string? href);

    Task EmailSendAsync(string email, string subject, string htmlMessage);
}
