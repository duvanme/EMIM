using EMIM.Models;
using EMIM.ViewModel;
using System.Threading.Tasks;

namespace EMIM.Services
{
    public interface IStoreService
    {
        Task<Store?> CreateStoreAsync(string userId, CreateStoreViewModel model, string? filePath);
        Task<bool> AssignVendorRoleAsync(User user);
    }
}
