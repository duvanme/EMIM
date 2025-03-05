using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Services
{
    //Define los servicios para la gestion de usuarios
    public interface IUserService
    {
        Task<User> FindUserByEmailAsync(string email);
        Task<IdentityResult> RegisterUserAsync(UserViewModel model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);
    }
}