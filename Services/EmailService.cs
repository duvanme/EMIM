using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EMIM.Services
{
    public class EmailService : IEmailService
    {
    
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


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

       
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = _configuration.GetValue<int>("EmailSettings:Port"),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:UserName"],
                        _configuration["EmailSettings:Password"]),
                    EnableSsl = _configuration.GetValue<bool>("EmailSettings:UseSSL")
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }

    }
}