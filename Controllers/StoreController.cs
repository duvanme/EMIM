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
        private readonly UserManager<User> userManager;

        public StoreController(IStoreService storeService, UserManager<User> userManager)
        {
            this.storeService = storeService;
            this.userManager = userManager;
        }

        public IActionResult StoreProfile() => View();
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

            string filePath = null;

            if (model.StoreProfilePicture != null && model.StoreProfilePicture.Length > 0)
            {

                var uploadsFolder = Path.Combine("wwwroot", "images", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StoreProfilePicture.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.StoreProfilePicture.CopyToAsync(fileStream);
                }


                filePath = Path.Combine("images", "uploads", uniqueFileName);
            }

            var store = await storeService.CreateStoreAsync(user.Id, model, filePath);
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
