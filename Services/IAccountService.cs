using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<User> FindUserByEmailAsync(string email);
    }
}
