using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class StoreService : IStoreService
    {
        private readonly EmimContext emimcontext;
        private readonly UserManager<User> userManager;


        public StoreService(EmimContext emimcontext, UserManager<User> userManager)
        {
            this.emimcontext = emimcontext;
            this.userManager = userManager;
        }

        public async Task<Store?> CreateStoreAsync(string userId, CreateStoreViewModel model, string? filePath)
        {


            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return null;


            var store = new Store
            {
                Name = model.Name,
                Description = model.Description,
                StoreStatus = "pending",
                UserId = userId,
                StoreProfilePicture = filePath
            };

            emimcontext.Stores.Add(store);
            await emimcontext.SaveChangesAsync();

            return store;
        }


        public async Task<Store?> AcceptCreateStore(Store model)
        {
            if (model != null)
            {
                model.StoreStatus = "enabled";//Cambia estado de la tienda a habilitada.

                emimcontext.SaveChanges();
            }
            return model;
        }

        public async Task<Store?> DenyCreateStore(Store model)
        {
            if (model != null)
            {
                model.StoreStatus = "denied";//Cambia estado de la tienda a habilitada.

                emimcontext.SaveChanges();
            }
            return model;
        }

        public async Task<int> GetStoreIdForVendorAsync(string userId)
        {
            // Eliminar la referencia a VendorId que no existe
            var store = await emimcontext.Stores
                .FirstOrDefaultAsync(s => s.UserId == userId);

            // Si se encuentra la tienda, devuelve su ID, si no, devuelve 0
            return store?.Id ?? 0;

        }

        public async Task<bool> AssignVendorRoleAsync(User user)
        {
            if (!await userManager.IsInRoleAsync(user, "Vendor"))
            {
                await userManager.RemoveFromRoleAsync(user, "Customer");
                await userManager.AddToRoleAsync(user, "Vendor");
                return true;
            }
            return false;
        }

        public async Task<Store> GetStoreDetailsAsync(int storeId)
        {
            return await emimcontext.Stores
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == storeId);
        }

    }

}
