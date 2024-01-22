namespace SignupSystem.Services.Auth.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string lastName, string firstName, string to, string subject, string body);
    }
}
