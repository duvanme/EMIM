using EMIM.ViewModel;

public interface IFavoriteService
{
    Task<bool> AddFavoriteAsync(string userId, int productId);
    Task<bool> RemoveFavoriteAsync(string userId, int productId);
    Task<bool> IsFavoriteAsync(string userId, int productId);
    Task<List<ProductViewModel>> GetFavoriteProductsAsync(string userId);
    Task<int> GetFavoriteCountAsync(string userId);
}