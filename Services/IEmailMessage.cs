namespace EMIM.Services
{
    public interface IEmailMessage
    {
        Task SendEmailAsync(EmailMessage message);
    }
}