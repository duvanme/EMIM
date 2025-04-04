using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class ProductService : IProductService
    {
        private readonly EmimContext _context;

        public ProductService(EmimContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Store).ToListAsync();
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                StoreId = product.StoreId,
                ImageUrl = product.ImageUrl,
                StoreName = product.Store?.Name ?? "Tienda Desconocida"

            };
        }

        public async Task<List<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    StoreId = p.StoreId,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
        }

        public async Task<bool> CreateProductAsync(ProductViewModel productVM)
        {
            var product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                Quantity = productVM.Quantity,
                CategoryId = productVM.CategoryId,
                StoreId = productVM.StoreId,
                ImageUrl = productVM.ImageUrl
            };

            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Primero, buscar el producto
                    var product = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == productId);

                    if (product == null)
                    {
                        Console.WriteLine($"Producto con ID {productId} no encontrado");
                        return false;
                    }

                    // Eliminar primero las preguntas asociadas al producto
                    var relatedQuestions = await _context.Questions
                        .Where(q => q.IdProducto == productId)
                        .ToListAsync();

                    if (relatedQuestions.Any())
                    {
                        _context.Questions.RemoveRange(relatedQuestions);
                        await _context.SaveChangesAsync();
                    }

                    // Ahora eliminar el producto
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    await transaction.RollbackAsync();

                    // Log detallado del error
                    Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                    Console.WriteLine($"Traza de la pila: {ex.StackTrace}");

                    // Si es un error de base de datos, imprimir detalles del error interno
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Error interno: {ex.InnerException.Message}");
                    }

                    return false;
                }
            }
        }


        public async Task<bool> UpdateProductAsync(ProductViewModel productVM)
        {
            var product = await _context.Products.FindAsync(productVM.Id);
            if (product == null) return false;

            product.Name = productVM.Name;
            product.Description = productVM.Description;
            product.Price = productVM.Price;
            product.Quantity = productVM.Quantity;
            product.CategoryId = productVM.CategoryId;
            product.StoreId = productVM.StoreId;

            if (!string.IsNullOrEmpty(productVM.ImageUrl))
            {
                product.ImageUrl = productVM.ImageUrl;
            }

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<HighlightedProductViewModel>> GetHighlightedProductsAsync()
        {

            return await _context.Products
           .Where(p => p.IsHighlighted)
           .Take(10)
           .Select(p => new HighlightedProductViewModel
           {
               Id = p.Id,
               Name = p.Name,
               Description = p.Description,
               Price = p.Price,
               Quantity = p.Quantity,
               CategoryId = p.CategoryId,
               StoreId = p.StoreId,
               ImageUrl = p.ImageUrl,
               StoreName = p.Store != null ? p.Store.Name : string.Empty
           })
           .ToListAsync();
        }


        public async Task<bool> StoreExistsAsync(int storeId)
        {
            return await _context.Stores.AnyAsync(s => s.Id == storeId);
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Store> GetStores()
        {
            return _context.Stores.ToList();
        }

        public async Task<List<Store>> GetStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<bool> IsProductOwnedByStoreAsync(int productId, int storeId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product != null && product.StoreId == storeId;
        }

        public async Task<List<ProductViewModel>> GetProductsByStoreIdAsync(int storeId)
        {
            return await _context.Products
                .Where(p => p.StoreId == storeId)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageUrl = p.ImageUrl,
                    StoreId = p.StoreId,
                    StoreName = p.Store.Name
                })
                .ToListAsync();
        }
    }
}
