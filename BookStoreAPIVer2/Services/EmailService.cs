using System.Net;
using System.Net.Mail;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string customerEmail)
    {
        var smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("MAIL_HOST"))
        {
            Port = 587,
            Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("MAIL_USERNAME"), Environment.GetEnvironmentVariable("MAIL_PASSWORD")),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("no-reply@bookstore.com"),
            Subject = "Your Order",
            Body = "<p>Thank you for your order!</p>",
            IsBodyHtml = true
        };
        
        mailMessage.To.Add(customerEmail);

        return smtpClient.SendMailAsync(mailMessage);
    }
}