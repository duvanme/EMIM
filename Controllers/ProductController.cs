using EMIM.Data;
using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IQuestionService _questionService; // Añade esto
        private readonly IStoreService _storeService;
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favoriteService;
        private readonly ISaleOrderService _orderService;
        private readonly EmimContext _context; // o el nombre que uses para tu DbContext


        public ProductController(
            IProductService productService,
            IQuestionService questionService,
            IStoreService storeService,
            UserManager<User> userManager,
            ISaleOrderService orderService,
            EmimContext context,
            IFavoriteService favoriteService) // Añade este parámetro
        {
            _productService = productService;
            _questionService = questionService; // Añade esta línea
            _storeService = storeService;
            _userManager = userManager;
            _favoriteService = favoriteService;
            _orderService = orderService;
            _context = context;

        }

        public async Task<IActionResult> ProductosBloqueados()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDisplay(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound($"No se encontró el producto con ID {id}");

            // Verificar si es favorito
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                product.IsFavorite = await _favoriteService.IsFavoriteAsync(userId, id);
            }

            var answeredQuestions = await _questionService.GetAnsweredQuestionsByProductIdAsync(id);
            ViewBag.AnsweredQuestions = answeredQuestions;

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> HighlightedProductDisplay()
        {
            var highlightedProducts = await _productService.GetHighlightedProductsAsync();
            return ViewComponent("HighlightedProducts", highlightedProducts);

        }


        public async Task<IActionResult> NewProduct()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var storeId = await _storeService.GetStoreIdForVendorAsync(user.Id);

            ViewBag.Categories = _productService.GetCategories();
            ViewBag.Stores = _productService.GetStores();

            var model = new ProductViewModel
            {
                StoreId = storeId // Asignar el ID de la tienda automáticamente
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _productService.GetCategories();
                ViewBag.Stores = _productService.GetStores();
                return View("NewProduct", productVM);
            }

            if (productVM.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productVM.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productVM.ImageFile.CopyToAsync(fileStream);
                }

                productVM.ImageUrl = "/images/" + fileName;
            }

            var success = await _productService.CreateProductAsync(productVM);
            if (!success)
            {
                ModelState.AddModelError("", "Error al crear el producto.");
                ViewBag.Categories = _productService.GetCategories();
                ViewBag.Stores = _productService.GetStores();
                return View("NewProduct", productVM);
            }

            return RedirectToAction("StoreProfile", "Store", new { id = productVM.StoreId });
        }


        public async Task<IActionResult> EditProduct(int id)
        {
            var productVM = await _productService.GetProductByIdAsync(id);
            if (productVM == null) return NotFound();

            // Verificar que el usuario actual tiene permisos para editar este producto
            var user = await _userManager.GetUserAsync(User);
            var userStoreId = await _storeService.GetStoreIdForVendorAsync(user.Id);

            if (userStoreId != productVM.StoreId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            ViewBag.Categories = _productService.GetCategories();

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _productService.GetCategories();
                return View("EditProduct", productVM);
            }

            // Obtener el ID de tienda original del producto
            var originalProduct = await _productService.GetProductByIdAsync(productVM.Id);
            if (originalProduct == null)
            {
                return NotFound();
            }

            // Asegurarse de que no se cambie la tienda
            productVM.StoreId = originalProduct.StoreId;

            // Verificar que el usuario actual tiene permisos para editar productos de esta tienda
            var user = await _userManager.GetUserAsync(User);
            var userStoreId = await _storeService.GetStoreIdForVendorAsync(user.Id);

            if (userStoreId != productVM.StoreId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Procesar la imagen si se ha subido una nueva
            if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productVM.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productVM.ImageFile.CopyToAsync(fileStream);
                }

                productVM.ImageUrl = "/images/" + fileName;
            }
            else
            {
                productVM.ImageUrl = originalProduct.ImageUrl;
            }

            var success = await _productService.UpdateProductAsync(productVM);
            if (!success)
            {
                ModelState.AddModelError("", "⚠️ Error al actualizar el producto en la base de datos.");
                ViewBag.Categories = _productService.GetCategories();
                return View("EditProduct", productVM);
            }

            return RedirectToAction("StoreProfile", "Store", new { id = productVM.StoreId });
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = _userManager.GetUserId(User); // método que se usa para obtener el usuario actual
            var orders = await _context.SaleOrders
                .Include(o => o.SaleOrderLine)
                .ThenInclude(line => line.Product)
                .Include(o => o.SaleOrderStatus)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.SaleOrders
                .Include(o => o.SaleOrderLine)
                .ThenInclude(line => line.Product)
                .Include(o => o.SaleOrderStatus)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return ViewComponent("ProductCard", new { categoryId = categoryId, products = products });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);

                if (result)
                {
                    return Ok(new { message = "Producto eliminado exitosamente" });
                }

                return BadRequest(new
                {
                    message = "No se pudo eliminar el producto",
                    details = "Verificar si el producto existe"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteProduct: {ex.Message}");

                return StatusCode(500, new
                {
                    message = "Error interno al eliminar el producto",
                    details = ex.Message
                });
            }
        }
    }
}