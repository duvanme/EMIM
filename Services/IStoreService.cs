using EMIM.Models;
using EMIM.ViewModel;
using System.Threading.Tasks;

namespace EMIM.Services
{
    public interface IStoreService
    {
        Task<Store?> CreateStoreAsync(string userId, CreateStoreViewModel model, string? filePath);
        Task<Store?> AcceptCreateStore(Store model);
        Task<Store?> DenyCreateStore(Store model);
        Task<bool> AssignVendorRoleAsync(User user);
        // Nuevo método para obtener el StoreId de un vendedor
        Task<int> GetStoreIdForVendorAsync(string userId);
        Task<Store> GetStoreDetailsAsync(int storeId);
        Task<bool> UpdateStoreAsync(EditStoreViewModel model, string? storePicturePath);
    }
}
