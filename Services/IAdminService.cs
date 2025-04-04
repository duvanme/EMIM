using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Services
{
    //Define los servicios para la gestion de usuarios
    public interface IAdminService
    {
        Task SendMailAsync(string email, string subject, string message);
        Task<IdentityResult> RegisterUserAsync(AdminViewModel model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<User> FindUserByEmailAsync(string email);

         Task SendEmailAsync(string email, string subject, string message);
        //Task<string> GenerateRandomPassword();
    }
}
