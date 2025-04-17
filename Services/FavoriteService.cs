using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{

    public class FavoriteService : IFavoriteService
    {
        private readonly EmimContext _context;

        public FavoriteService(EmimContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFavoriteAsync(string userId, int productId)
        {
            // Verificar si ya existe
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (existingFavorite != null)
                return true; // Ya está en favoritos

            var favorite = new Favorite
            {
                UserId = userId,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFavoriteAsync(string userId, int productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite == null)
                return false;

            _context.Favorites.Remove(favorite);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsFavoriteAsync(string userId, int productId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }

        public async Task<List<ProductViewModel>> GetFavoriteProductsAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Product)
                .ThenInclude(p => p.Store)
                .Select(f => new ProductViewModel
                {
                    Id = f.Product.Id,
                    Name = f.Product.Name,
                    Description = f.Product.Description,
                    Price = f.Product.Price,
                    Quantity = f.Product.Quantity,
                    CategoryId = f.Product.CategoryId,
                    StoreId = f.Product.StoreId,
                    ImageUrl = f.Product.ImageUrl,
                    StoreName = f.Product.Store != null ? f.Product.Store.Name : "Tienda Desconocida"
                })
                .ToListAsync();
        }

        public async Task<int> GetFavoriteCountAsync(string userId)
        {
            return await _context.Favorites
                .CountAsync(f => f.UserId == userId);
        }
    }
}