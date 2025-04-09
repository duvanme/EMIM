// SaleOrderStatusService.cs
using EMIM.Data;
using EMIM.Models;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class SaleOrderStatusService : ISaleOrderStatusService
    {
        private readonly EmimContext _context;

        public SaleOrderStatusService(EmimContext context)
        {
            _context = context;
        }

        public async Task CreateInitialStatusAsync(int orderId, string statusName)
        {
            // Verificar si la orden existe
            var orderExists = await _context.SaleOrders.AnyAsync(o => o.Id == orderId);
            if (!orderExists)
            {
                throw new ArgumentException($"La orden con ID {orderId} no existe");
            }

            var orderStatus = new SaleOrderStatus
            {
                Name = statusName,
                CreatedAt = DateTime.UtcNow,
                OrderId = orderId
            };

            _context.SaleOrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();
        }
    }
}