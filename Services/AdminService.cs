using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EMIM.Services
{
    public class AdminService : IAdminService
    {
        //Inyeccion de dependencias para usuarios, roles y base de datos
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EmimContext _context;

        public AdminService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, EmimContext context)
        {
            //Constructor que inicializa las depedencias 
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }


        private async Task SendMailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("EMIM System", "4dminemim@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = $"<p>{message}</p>"};
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    Console.WriteLine("Conectando al servidor SMTP...");
                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect); //Usa SSL
                    Console.WriteLine("Conexión establecida");

                    await client.AuthenticateAsync("4dm1nemim@gmail.com", "wqtywlfwbjxvvcib"); //Usa la contraseña de aplicación
                    Console.WriteLine("Autenticación exitosa");

                    await client.SendAsync(emailMessage);
                    Console.WriteLine("Correo enviado correctamente");

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                throw new Exception("Error al enviar el correo. Revisa tu configuración SMTP.");
            } 
        }

        //Generar contraseña aleatoria 
        private string GenerateRandomPassword()
        {
            int length = 12;
            string upperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            string lowerCase = "abcdefghijkmnopqrstuvwxyz";
            string numbers = "0123456789";
            string specialChars = "!@$?_-.";

            string allChars = upperCase + lowerCase + numbers + specialChars;

            Random rand = new Random();
            List<char> password = new List<char> //Lista para almacenar los caracteres de la contraseña
            {
                upperCase[rand.Next(upperCase.Length)],  // Asegurar una mayúscula
                lowerCase[rand.Next(lowerCase.Length)],  // Asegurar una minúscula
                numbers[rand.Next(numbers.Length)],      // Asegurar un número
                specialChars[rand.Next(specialChars.Length)] // Asegurar un carácter especial
            };

            // Completar la contraseña hasta la longitud requerida
            for (int i = 4; i < length; i++)
            {
                // Agregar un carácter aleatorio del conjunto elegido
                password.Add(allChars[rand.Next(allChars.Length)]);
            }

            // Devolver la contraseña como una cadena, ordenando los caracteres aleatoriamente
            return new string(password.OrderBy(x => rand.Next()).ToArray());
        }


        //Registrar un nuvo usuario en el sistema
        public async Task<IdentityResult> RegisterUserAsync(AdminViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email, // Asegúrate de que UserName no sea nulo
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var password = GenerateRandomPassword(); // Generar la contraseña automáticamente
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                try
                {
                    Console.WriteLine("Enviando correo...");
                    // Enviar correo con la contraseña generada
                    string mensaje = $"Welcome to EMIM. Your user is <strong>{model.Email}</strong> and your password is <strong>{password}</strong>";
                    await SendMailAsync(model.Email, "Registration successful", mensaje);
                    Console.WriteLine("Correo enviado.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Failed sending mail: {ex.Message}");
                }

                // Asignar rol
                var roleName = model.Role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!createRoleResult.Succeeded)
                    {
                        // Manejar el error al crear el rol
                        foreach (var error in createRoleResult.Errors)
                        {
                            Console.WriteLine($"Failed to create role: {error.Description}");
                        }
                    }
                }

                var addToRoleResult = await userManager.AddToRoleAsync(user, roleName);
                if (!addToRoleResult.Succeeded)
                {
                    // Manejar el error al asignar el rol
                    foreach (var error in addToRoleResult.Errors)
                    {
                        Console.WriteLine($"Failed to assign role: {error.Description}");
                    }
                }
            }

            return result;
        }

        //Metodo para cambiar la contraseña
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            // Se elimina la contraseña actual antes de agregar la nueva
            var removeResult = await userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return removeResult;

            // Se agrega la nueva contraseña al usuario
            return await userManager.AddPasswordAsync(user, model.NewPassword);
        }


        //Metodo para buscar un usuario por correo electrónico
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        Task IAdminService.SendEmailAsync(string email, string subject, string message)
        {
            return SendMailAsync(email, subject, message);
        }

        Task IAdminService.SendMailAsync(string email, string subject, string message)
        {
            return SendMailAsync(email, subject, message);
        }

        /* Task<string> IAdminService.GenerateRandomPassword()
        {
            return GenerateRandomPassword();
        } */
    }
}
