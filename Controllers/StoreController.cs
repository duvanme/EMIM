using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {

        private readonly IStoreService storeService;
        private readonly IProductService _productService;
        private readonly UserManager<User> userManager;

        public StoreController(IStoreService storeService, UserManager<User> userManager, IProductService productService)
        {
            this.storeService = storeService;
            this.userManager = userManager;
            _productService = productService;
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


        public IActionResult TiendasBloqueadas() => View();

         public IActionResult QuestionsAnswers() => View();

    }

}
