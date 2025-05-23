using EMIM.Models;
using EMIM.ViewModel;

namespace EMIM.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(int id);
        Task<bool> CreateProductAsync(ProductViewModel productVM);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(ProductViewModel productVM);
        Task<bool> StoreExistsAsync(int storeId);
        List<Category> GetCategories();
        List<Store> GetStores();
        Task<List<Store>> GetStoresAsync();
        Task<bool> IsProductOwnedByStoreAsync(int productId, int storeId);
        Task<List<ProductViewModel>> GetProductsByStoreIdAsync(int storeId);
        Task<IEnumerable<HighlightedProductViewModel>> GetHighlightedProductsAsync();
        Task<List<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
        Task<int> GetProductStoreIdAsync(int productId);
    }
}
