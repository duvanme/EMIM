// ISaleOrderService.cs
using EMIM.Models;
using EMIM.ViewModel;

namespace EMIM.Services
{
    public interface ISaleOrderService{
    Task<SaleOrder> CreateSaleOrderAsync(string userId, decimal totalAmount);
    Task<string?> GetOrdersByUserIdAsync(string id);
    Task<SaleOrder> GetSaleOrderByIdAsync(int id);
    Task<List<SaleOrder>> GetUserOrdersAsync(string userId);
    Task<OrderDetailViewModel> GetOrderDetailsAsync(int orderId);
    Task<List<SaleOrder>> GetStoreOrdersAsync(int storeId);
}
