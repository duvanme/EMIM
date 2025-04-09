// ISaleOrderService.cs
using EMIM.Models;

namespace EMIM.Services
{
    public interface ISaleOrderService
{
    Task<SaleOrder> CreateSaleOrderAsync(string userId, decimal totalAmount);
    Task<SaleOrder> GetSaleOrderByIdAsync(int id);
}
}