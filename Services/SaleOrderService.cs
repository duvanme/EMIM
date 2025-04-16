// SaleOrderService.cs
using EMIM.Data;
using EMIM.Models;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly EmimContext _context;

        public SaleOrderService(EmimContext context)
        {
            _context = context;
        }

        public async Task<SaleOrder> CreateSaleOrderAsync(string userId, decimal totalAmount)
        {
            var saleOrder = new SaleOrder
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                TotalPrice = totalAmount
            };

            _context.SaleOrders.Add(saleOrder);
            await _context.SaveChangesAsync();

            return saleOrder;
        }

        public async Task<SaleOrder> GetSaleOrderByIdAsync(int id)
        {
            return await _context.SaleOrders
                .Include(so => so.SaleOrderLine)
                .Include(so => so.SaleOrderStatus)
                .FirstOrDefaultAsync(so => so.Id == id);
        }

        public async Task<List<SaleOrder>> GetOrdersByUserAsync(string userId)
        {
            return await _context.SaleOrders
                .Include(o => o.SaleOrderLine)
                .ThenInclude(line => line.Product)
                .Include(o => o.SaleOrderStatus)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }


        Task<string?> ISaleOrderService.GetOrdersByUserIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}