using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMIM.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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

            return View(product);
        }


        public IActionResult NewProduct()
        {
            ViewBag.Categories = _productService.GetCategories();
            ViewBag.Stores = _productService.GetStores();
            return View(new ProductViewModel());
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

            ViewBag.Categories = _productService.GetCategories();
            ViewBag.Stores = _productService.GetStores();

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _productService.GetCategories();
                ViewBag.Stores = new SelectList(await _productService.GetStoresAsync(), "Id", "Name");
                return View("EditProduct", productVM);
            }

            bool storeExists = await _productService.StoreExistsAsync(productVM.StoreId);
            if (!storeExists)
            {
                ModelState.AddModelError("StoreId", "La tienda seleccionada no es válida.");
                ViewBag.Categories = _productService.GetCategories();
                ViewBag.Stores = new SelectList(await _productService.GetStoresAsync(), "Id", "Name");
                return View("EditProduct", productVM);
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
            else
            {
                var existingProduct = await _productService.GetProductByIdAsync(productVM.Id);
                productVM.ImageUrl = existingProduct?.ImageUrl;
            }

            var success = await _productService.UpdateProductAsync(productVM);
            if (!success)
            {
                ModelState.AddModelError("", "⚠️ Error al actualizar el producto en la base de datos.");
                ViewBag.Categories = _productService.GetCategories();
                ViewBag.Stores = new SelectList(await _productService.GetStoresAsync(), "Id", "Name");
                return View("EditProduct", productVM);
            }

            return RedirectToAction("StoreProfile", "Store", new { id = productVM.StoreId });
        }

    }
}
