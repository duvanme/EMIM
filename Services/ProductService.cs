using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModels;
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
                ImageUrl = product.ImageUrl
            };
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

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
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
    }
}
