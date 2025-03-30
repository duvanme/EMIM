using EMIM.Models;
using EMIM.ViewModel;

namespace EMIM.Services
{
    public interface IUserService
    {
        Task<EditUserProfileViewModel> FindUserByIdAsync(string id);
        
    }
}
