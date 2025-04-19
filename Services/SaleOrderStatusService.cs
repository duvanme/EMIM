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

        // SaleOrderStatusService.cs - Añadir estos métodos
        public async Task<string> GetCurrentStatusAsync(int orderId)
        {
            var latestStatus = await _context.SaleOrderStatuses
                .Where(sos => sos.OrderId == orderId)
                .OrderByDescending(sos => sos.CreatedAt)
                .FirstOrDefaultAsync();

            return latestStatus?.Name ?? "Pagado"; // Estado por defecto
        }

        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            // Verificar si la orden existe
            var orderExists = await _context.SaleOrders.AnyAsync(o => o.Id == orderId);
            if (!orderExists)
            {
                throw new ArgumentException($"La orden con ID {orderId} no existe");
            }

            // Crear un nuevo registro de estado
            var orderStatus = new SaleOrderStatus
            {
                Name = newStatus,
                CreatedAt = DateTime.UtcNow,
                OrderId = orderId
            };

            _context.SaleOrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();
        }
    }
}