using EMIM.Data;
using EMIM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // Asegurar que esta librería esté incluida

namespace EMIM.Controllers
{
    public class ProductController : Controller
    {
        private readonly EmimContext _context;

        // ✅ Constructor con inyección de dependencias
        public ProductController(EmimContext context)
        {
            _context = context;
        }

        public IActionResult ProductDisplay() => View();

        public IActionResult NewProduct()
        {
            ViewData["Categories"] = _context.Categories.ToList(); // Mejor que ViewBag
            return View();
        }

        public IActionResult EditProduct() => View();
        public IActionResult ProductosBloqueados() => View();

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            Console.WriteLine($"Intentando guardar el producto: \n" +
                              $"Nombre: {product.Name}\n" +
                              $"Descripción: {product.Description}\n" +
                              $"Precio: {product.Price}\n" +
                              $"Cantidad: {product.Quantity}\n" +
                              $"Categoría: {product.CategoryId}\n" +
                              $"Tienda: {product.StoreId}\n");

            // Buscar los objetos relacionados
            product.Category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            product.Store = _context.Stores.FirstOrDefault(s => s.Id == product.StoreId);

            if (product.Category == null)
            {
                ModelState.AddModelError("Category", "La categoría seleccionada no existe.");
                Console.WriteLine("❌ Error: La categoría no existe.");
            }

            if (product.Store == null)
            {
                ModelState.AddModelError("Store", "La tienda seleccionada no existe.");
                Console.WriteLine("❌ Error: La tienda no existe.");
            }

            // 🔹 Verifica si hay una imagen cargada
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // 🔹 Crea la carpeta si no existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 🔹 Guarda la imagen en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                // 🔹 Guarda la ruta en la base de datos
                product.ImageUrl = "/images/" + uniqueFileName;
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }

                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Stores = _context.Stores.ToList();
                return View("NewProduct", product);
            }

            try
            {
                _context.Products.Add(product);
                int changes = await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Producto guardado con éxito. Registros guardados: {changes}");
                return RedirectToAction("ProductDisplay");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar en la BD: {ex.Message}");
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Stores = _context.Stores.ToList();
                ModelState.AddModelError("", "Ocurrió un error al guardar el producto.");
                return View("NewProduct", product);
            }
        }
    }
}
