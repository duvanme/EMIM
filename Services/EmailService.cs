using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EMIM.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Credentials = new NetworkCredential("4dm1nemim@gmail.com", "kcjuqxhhcjknifop");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587; // O el puerto que uses

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("4dm1nemim@gmail.com"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}