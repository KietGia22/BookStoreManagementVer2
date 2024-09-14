namespace BookStoreAPIVer2.Services.IService;

public interface IEmailService
{
    Task SendEmailAsync(string customerEmail);
}