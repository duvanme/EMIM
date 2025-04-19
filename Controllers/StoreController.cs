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

        private readonly ISaleOrderService _saleOrderService;
        private readonly ISaleOrderStatusService _statusService;

        public StoreController(IStoreService storeService, UserManager<User> userManager, IProductService productService, EmimContext emimcontext, ISaleOrderService saleOrderService,
        ISaleOrderStatusService statusService)
        {
            this.storeService = storeService;
            this.userManager = userManager;
            _productService = productService;
            this.emimcontext = emimcontext;
            _saleOrderService = saleOrderService;
            _statusService = statusService;
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
        public async Task<IActionResult> CreateStore(CreateStoreViewModel model)
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

        [Authorize(Roles = "Vendor,Admin")]
        public async Task<IActionResult> Purchase(string statusFilter = "all", string sortOrder = "recent")
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

            // Obtener las órdenes que contienen productos de esta tienda
            var orders = await _saleOrderService.GetStoreOrdersAsync(storeId);

            // Convertir a ViewModels (igual que en OrderController.MyOrders)
            var orderViewModels = orders.Select(o =>
            {
                // Obtener el estado actual (el más reciente)
                var currentStatus = o.SaleOrderStatus
                    .OrderByDescending(sos => sos.CreatedAt)
                    .FirstOrDefault()?.Name ?? "Pagado";

                // Calculamos la fecha estimada de entrega
                var minDeliveryDate = o.CreatedAt.AddDays(7);
                var maxDeliveryDate = o.CreatedAt.AddDays(14);
                string estimatedDelivery;

                if (currentStatus.ToLower() == "completado")
                {
                    var completedDate = o.SaleOrderStatus
                        .Where(sos => sos.Name.ToLower() == "completado")
                        .OrderByDescending(sos => sos.CreatedAt)
                        .FirstOrDefault()?.CreatedAt ?? DateTime.UtcNow;

                    estimatedDelivery = $"Entregado el {completedDate:dd/MM/yyyy}";
                }
                else
                {
                    estimatedDelivery = $"{minDeliveryDate:dd}-{maxDeliveryDate:dd} de {minDeliveryDate:MMMM}";
                }

                // Obtener miniaturas de productos
                var productThumbnails = o.SaleOrderLine
                    .Take(3) // Solo las primeras 3 imágenes
                    .Select(line => new ProductThumbnailViewModel
                    {
                        ProductId = line.ProductId,
                        ImageUrl = line.Product?.ImageUrl ?? "/api/placeholder/50/50"
                    }).ToList();

                return new SaleOrderViewModel
                {
                    Id = o.Id,
                    CreatedAt = o.CreatedAt,
                    TotalPrice = o.TotalPrice,
                    CurrentStatus = currentStatus,
                    UserId = o.UserId,
                    UserName = o.User?.UserName ?? "Usuario",
                    ProductCount = o.SaleOrderLine.Count,
                    ProductThumbnails = productThumbnails,
                    EstimatedDeliveryDate = estimatedDelivery
                };
            }).ToList();

            // Aplicar filtros de estado
            if (statusFilter != "all")
            {
                orderViewModels = orderViewModels
                    .Where(o => o.CurrentStatus.ToLower() == statusFilter.ToLower())
                    .ToList();
            }

            // Aplicar ordenación
            orderViewModels = sortOrder switch
            {
                "older" => orderViewModels.OrderBy(o => o.CreatedAt).ToList(),
                "expensive" => orderViewModels.OrderByDescending(o => o.TotalPrice).ToList(),
                "cheaper" => orderViewModels.OrderBy(o => o.TotalPrice).ToList(),
                _ => orderViewModels.OrderByDescending(o => o.CreatedAt).ToList() // "recent" es el default
            };

            var viewModel = new OrdersListViewModel
            {
                Orders = orderViewModels,
                StatusFilter = statusFilter,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Vendor,Admin")]
        public async Task<IActionResult> PurchaseStatus(int id)
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

            // Verificar que la orden contiene productos de esta tienda
            var orderDetails = await _saleOrderService.GetOrderDetailsAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            // Verificar que la tienda tiene productos en esta orden
            bool hasProductsFromStore = orderDetails.Items.Any(item =>
                _productService.GetProductStoreIdAsync(item.ProductId).Result == storeId);

            if (!hasProductsFromStore && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(orderDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendor,Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            // Validar que el status sea uno de los permitidos
            var allowedStatuses = new[] { "Pagado", "Empacando", "En reparto", "Completado" };
            if (!allowedStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Estado no válido");
            }

            try
            {
                await _statusService.UpdateOrderStatusAsync(orderId, status);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }

}
