// SaleOrderService.cs
using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
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

        public async Task<List<SaleOrder>> GetUserOrdersAsync(string userId)
        {
            return await _context.SaleOrders
                .Include(so => so.SaleOrderLine)
                    .ThenInclude(sol => sol.Product)
                .Include(so => so.SaleOrderStatus)
                .Where(so => so.UserId == userId)
                .OrderByDescending(so => so.CreatedAt)
                .ToListAsync();
        }

        public async Task<OrderDetailViewModel> GetOrderDetailsAsync(int orderId)
        {
            var order = await _context.SaleOrders
                .Include(so => so.SaleOrderLine)
                    .ThenInclude(sol => sol.Product)
                        .ThenInclude(p => p.Store)
                .Include(so => so.SaleOrderStatus)
                .FirstOrDefaultAsync(so => so.Id == orderId);

            if (order == null)
                return null;

            // Obtener el estado actual (el más reciente)
            var currentStatus = order.SaleOrderStatus
                .OrderByDescending(sos => sos.CreatedAt)
                .FirstOrDefault();

            // Calculamos la fecha estimada de entrega (por ejemplo, entre 7-14 días desde la creación)
            var minDeliveryDate = order.CreatedAt.AddDays(7);
            var maxDeliveryDate = order.CreatedAt.AddDays(14);
            var estimatedDelivery = $"{minDeliveryDate:dd/MM}-{maxDeliveryDate:dd}";

            // Costo de envío fijo o calculado según lógica de negocio
            decimal shippingCost = 15.00m;

            var items = order.SaleOrderLine.Select(line => new OrderItemViewModel
            {
                ProductId = line.ProductId,
                ProductName = line.Product.Name,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                Subtotal = line.Subtotal,
                ImageUrl = line.Product.ImageUrl ?? "/api/placeholder/80/80",
                StoreName = line.Product.Store?.Name ?? "Tienda"
            }).ToList();

            return new OrderDetailViewModel
            {
                OrderId = order.Id,
                CreatedAt = order.CreatedAt,
                SubTotal = order.TotalPrice - shippingCost,
                ShippingCost = shippingCost,
                TotalPrice = order.TotalPrice,
                CurrentStatus = currentStatus?.Name ?? "Pagado",
                CurrentStatusId = GetStatusId(currentStatus?.Name ?? "Pagado"),
                EstimatedDeliveryDate = estimatedDelivery,
                Items = items
            };
        }

        private int GetStatusId(string status)
        {
            return status.ToLower() switch
            {
                "pagado" => 1,
                "empacando" => 2,
                "en reparto" => 3,
                "completado" => 4,
                _ => 1
            };
        }

        public async Task<List<SaleOrder>> GetStoreOrdersAsync(int storeId)
        {
            return await _context.SaleOrders
                .Include(so => so.SaleOrderLine)
                    .ThenInclude(sol => sol.Product)
                .Include(so => so.SaleOrderStatus)
                .Include(so => so.User)
                .Where(so => so.SaleOrderLine.Any(sol => sol.Product.StoreId == storeId))
                .OrderByDescending(so => so.CreatedAt)
                .ToListAsync();
        }
    }
}