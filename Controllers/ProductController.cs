using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductosBloqueados()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> ProductDisplay()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public IActionResult NewProduct()
        {
            // 🔹 Se cargan las categorías y tiendas en ViewBag antes de renderizar la vista
            ViewBag.Categories = _productService.GetCategories();
            ViewBag.Stores = _productService.GetStores();

            return View(new ProductViewModel()); // 🔹 Se pasa un ViewModel vacío a la vista
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

            // Procesar la imagen si se subió
            if (productVM.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // Asegurar que la carpeta existe
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

            return RedirectToAction("ProductDisplay");
        }

        public IActionResult EditProduct()
        {
            return View();
        }

    }
}
