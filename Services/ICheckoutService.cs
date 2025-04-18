using EMIM.DTOs;
using EMIM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EMIM.Services
{
    public interface ICheckoutService
    {
        Task<SaleOrder> ProcessSuccessfulPaymentAsync(string userId, List<CartItem> cartItems, decimal totalAmount);
    }
}