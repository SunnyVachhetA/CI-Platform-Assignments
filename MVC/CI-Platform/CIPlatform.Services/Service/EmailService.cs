using CIPlatform.Services.Service.Interface;
using MailKit.Net.Smtp;
using MimeKit;

namespace CIPlatform.Services.Service;
public class EmailService : IEmailService
{
    public void EmailSend(string email, string subject, string htmlMessage)
    {

        Console.WriteLine("Inside send email >>>>>>>>>>>>>");
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

    public void SendResetPasswordLink(string email, string? href)
    {

        string toEmail = email;
        string subject = "Reset Password for CI Platform";
        string message = $@"
                            <h2>Welcome Back,</h2>
                            Click below button to reset account's password!<br>
                            <a href='{href}'><button>Reset Your Password</button></a>  
                          ";

        Console.WriteLine("Forgot Password Message: " + message);
        EmailSend(toEmail, subject, message);
    }
}
