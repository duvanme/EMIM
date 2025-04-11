using EMIM.Data;
using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {

        private readonly IStoreService storeService;
        private readonly IProductService _productService;
        private readonly UserManager<User> userManager;
        private readonly EmimContext emimcontext;

        public StoreController(IStoreService storeService, UserManager<User> userManager, IProductService productService, EmimContext emimcontext)
        {
            this.storeService = storeService;
            this.userManager = userManager;
            _productService = productService;
            this.emimcontext = emimcontext;
        }

        public async Task<IActionResult> StoreProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            int storeId = await storeService.GetStoreIdForVendorAsync(user.Id);
            if (storeId == 0)
            {
                return RedirectToAction("CreateStore");
            }

            var store = await storeService.GetStoreDetailsAsync(storeId);

            // Añade un breakpoint o un log aquí para verificar el valor de StoreProfilePicture
            Console.WriteLine($"Store Profile Picture Path: {store.StoreProfilePicture}");

            var storeProducts = await _productService.GetProductsByStoreIdAsync(storeId);

            var viewModel = new StoreProfileViewModel
            {
                Store = new StoreViewModel
                {
                    Id = store.Id,
                    Name = store.Name,
                    Description = store.Description,
                    StoreProfilePicture = store.StoreProfilePicture, // Verifica que esto se está pasando correctamente
                    UserId = store.UserId,
                    User = store.User
                },
                Products = storeProducts
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult CreateStore() => View();

        [HttpPost]
        public async Task<IActionResult> CreateNewStore(CreateStoreViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid) return View(model);

            string storePicturePath = null;

            if (model.StoreProfilePicture != null && model.StoreProfilePicture.Length > 0)
            {
                // Usar WebRootPath para obtener la ruta base del wwwroot
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    "uploads"
                );

                // Crear la carpeta si no existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generar nombre de archivo único
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StoreProfilePicture.FileName;

                // Ruta completa del archivo
                var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

                // Guardar el archivo
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.StoreProfilePicture.CopyToAsync(fileStream);
                }

                // Guardar la ruta relativa para la base de datos
                storePicturePath = Path.Combine("images", "uploads", uniqueFileName);
            }

            var store = await storeService.CreateStoreAsync(user.Id, model, storePicturePath);
            if (store == null)
            {
                TempData["Error"] = "You already have a store!";
            }
            else
            {
                TempData["Success"] = "Store created successfully!";
            }

            await storeService.AssignVendorRoleAsync(user);

            return RedirectToAction("UserProfile", "User", new { id = user.Id });
        }

        public async Task<bool> UpdateStoreAsync(EditStoreViewModel model, string? storePicturePath)
        {
            var store = await emimcontext.Stores.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (store == null) return false;

            store.Description = model.Description;
            store.Location = model.Location;

            if (!string.IsNullOrEmpty(storePicturePath))
            {
                store.StoreProfilePicture = storePicturePath;
            }

            emimcontext.Stores.Update(store);
            await emimcontext.SaveChangesAsync();

            return true;
        }

        [HttpGet]
        public async Task<IActionResult> EditStore(int id)
        {
            var store = await emimcontext.Stores.FindAsync(id);
            if (store == null)
                return NotFound();

            var model = new EditStoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location ?? "",
                Description = store.Description ?? "",
                StoreProfilePicturePath = store.StoreProfilePicture, //Para mantener si no cambia 
                ExistingProfilePicturePath = store.StoreProfilePicture // Para mostrar en vista
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(EditStoreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string? storePicturePath = model.StoreProfilePicturePath;

            if (model.StoreProfilePicture != null && model.StoreProfilePicture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StoreProfilePicture.FileName;
                var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.StoreProfilePicture.CopyToAsync(fileStream);
                }

                storePicturePath = Path.Combine("images", "uploads", uniqueFileName);
            }

            var updated = await storeService.UpdateStoreAsync(model, storePicturePath);
            if (!updated)
                return NotFound();

            return RedirectToAction("StoreProfile");
        }

        public IActionResult TiendasBloqueadas() => View();

        public IActionResult QuestionsAnswers() => View();

    }

}
