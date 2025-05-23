using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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

            var storeSearchNit = emimcontext.Stores.FirstOrDefault(s => s.Nit == model.Nit);
            var storeSearchUser = emimcontext.Stores.FirstOrDefault(s => s.UserId == user.Id);

            if (storeSearchNit == null && storeSearchUser == null)
            {
                var store = new Store
                {
                    Name = model.Name,
                    Description = model.Description,
                    Nit = model.Nit,
                    Location = model.Location,
                    StoreStatus = "pending",
                    UserId = userId,
                    ImageUrl = model.ImageUrl,
                    StoreProfilePicture = filePath
                };

                emimcontext.Stores.Add(store);
                await emimcontext.SaveChangesAsync();

                return store;
            }else return null;
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
            var store = await emimcontext.Stores.FindAsync(model.Id);
            if (store != null)
            {
                emimcontext.Stores.Remove(store);
                await emimcontext.SaveChangesAsync();
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

        public async Task<bool> UpdateStoreAsync(EditStoreViewModel model, string? storePicturePath)
        {
            var store = await emimcontext.Stores.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (store == null)
                return false;

            store.Name = model.Name;
            store.Description = model.Description;
            store.Location = model.Location;
            store.StoreProfilePicture = storePicturePath ?? model.StoreProfilePicturePath;
            if (!string.IsNullOrEmpty(storePicturePath))
            {
                store.StoreProfilePicture = storePicturePath;
            }

            emimcontext.Stores.Update(store);
            await emimcontext.SaveChangesAsync();
            return true;
        }


    }

}
