// ISaleOrderLineService.cs
using EMIM.DTOs;
using EMIM.Models;

namespace EMIM.Services
{
    public interface ISaleOrderLineService
{
    Task AddOrderLinesAsync(int saleOrderId, List<CartItem> cartItems);
}
}