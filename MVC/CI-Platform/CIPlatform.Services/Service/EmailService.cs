using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;
using MailKit.Net.Smtp;
using MimeKit;

namespace CIPlatform.Services.Service;
public class EmailService : IEmailService
{
    public void EmailSend(string email, string subject, string htmlMessage)
    {

        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("tatvatestemail@gmail.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        //Send email
        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            emailClient.Authenticate("tatvatestemail@gmail.com", "dtiapgzevenaxnsw");
            emailClient.Send(emailToSend);
            emailClient.Disconnect(true);
        }
    }


    public async Task EmailSendAsync(string email, string subject, string htmlMessage)
    {
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("tatvatestemail@gmail.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        //Send email
        using (var emailClient = new SmtpClient())
        {
            await emailClient.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await emailClient.AuthenticateAsync("tatvatestemail@gmail.com", "dtiapgzevenaxnsw");
            await emailClient.SendAsync(emailToSend);
            await emailClient.DisconnectAsync(true);
        }
    }


    public void SendResetPasswordLink(string email, string? href)
    {
        string subject = "Reset Password Request - CI Platform";
        string message = MailMessageFormatUtility.GenerateMessageForResetPassword(href!);
        EmailSend(email, subject, message);
    }
}
