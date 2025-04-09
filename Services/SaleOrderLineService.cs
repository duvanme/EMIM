// SaleOrderLineService.cs
using EMIM.Data;
using EMIM.DTOs;
using EMIM.Models;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Services
{
    public class SaleOrderLineService : ISaleOrderLineService
    {
        private readonly EmimContext _context;

        public SaleOrderLineService(EmimContext context)
        {
            _context = context;
        }

        public async Task AddOrderLinesAsync(int saleOrderId, List<CartItem> cartItems)
        {
            // Verificar que la orden existe
            var order = await _context.SaleOrders.FindAsync(saleOrderId);
            if (order == null)
            {
                throw new ArgumentException($"La orden con ID {saleOrderId} no existe");
            }

            // Crear líneas de orden para cada producto en el carrito
            foreach (var item in cartItems)
            {
                // Verificar que el producto existe
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"El producto con ID {item.ProductId} no existe");
                }

                var orderLine = new SaleOrderLine
                {
                    ProductId = item.ProductId,
                    SaleOrderId = saleOrderId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };

                _context.SaleOrderLines.Add(orderLine);
            }

            await _context.SaveChangesAsync();
        }
    }
}