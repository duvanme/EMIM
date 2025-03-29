using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<EditUserProfileViewModel> FindUserByIdAsync(string id)
        {
            var user = await _userManager.Users
            .Where(u => u.Id == id)
            .Select(u => new EditUserProfileViewModel
            {   
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DateOfBirth = u.DateOfBirth
            })
            .FirstOrDefaultAsync();

            return user ?? throw new Exception("User not found");
        }
    }
}
