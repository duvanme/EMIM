// CheckoutService.cs
using EMIM.Data;
using EMIM.DTOs;
using EMIM.Models;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly EmimContext _context;
        private readonly ISaleOrderService _saleOrderService;
        private readonly ISaleOrderLineService _saleOrderLineService;
        private readonly ISaleOrderStatusService _saleOrderStatusService;

        public CheckoutService(
            EmimContext context,
            ISaleOrderService saleOrderService,
            ISaleOrderLineService saleOrderLineService,
            ISaleOrderStatusService saleOrderStatusService)
        {
            _context = context;
            _saleOrderService = saleOrderService;
            _saleOrderLineService = saleOrderLineService;
            _saleOrderStatusService = saleOrderStatusService;
        }

        public async Task<SaleOrder> ProcessSuccessfulPaymentAsync(string userId, List<CartItem> cartItems, decimal totalAmount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Crear la orden principal
                var saleOrder = new SaleOrder
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId,
                    TotalPrice = totalAmount
                };

                _context.SaleOrders.Add(saleOrder);
                await _context.SaveChangesAsync(); // Guardar para obtener el ID

                // 2. Agregar líneas de orden una por una
                foreach (var item in cartItems)
                {
                    var orderLine = new SaleOrderLine
                    {
                        ProductId = item.ProductId,
                        SaleOrderId = saleOrder.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    };

                    _context.SaleOrderLines.Add(orderLine);
                }
                await _context.SaveChangesAsync();

                // 3. Agregar estado inicial
                var orderStatus = new SaleOrderStatus
                {
                    Name = "Pagado",
                    CreatedAt = DateTime.UtcNow,
                    OrderId = saleOrder.Id
                };

                _context.SaleOrderStatuses.Add(orderStatus);
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                return saleOrder;
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                await transaction.RollbackAsync();
                Console.WriteLine($"Error en ProcessSuccessfulPaymentAsync: {ex.Message}");
                throw; // Re-lanzar la excepción para que se maneje en el controlador
            }
        }
    }
}