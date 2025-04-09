// ICheckoutService.cs
using EMIM.DTOs;
using EMIM.Models;

namespace EMIM.Services
{
    public interface ICheckoutService
    {
        Task<SaleOrder> ProcessSuccessfulPaymentAsync(string userId, List<CartItem> cartItems, decimal totalAmount);
    }
}