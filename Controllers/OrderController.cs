using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMIM.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly ISaleOrderStatusService _statusService;
        private readonly UserManager<User> _userManager;
        private readonly IProductService _productService;

        public OrderController(
            ISaleOrderService saleOrderService,
            ISaleOrderStatusService statusService,
            UserManager<User> userManager,
            IProductService productService)
        {
            _saleOrderService = saleOrderService;
            _statusService = statusService;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> MyOrders(string statusFilter = "all", string sortOrder = "recent")
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orders = await _saleOrderService.GetUserOrdersAsync(user.Id);
            
            // Convertir a ViewModels
            var orderViewModels = orders.Select(o => {
                // Obtener el estado actual (el más reciente)
                var currentStatus = o.SaleOrderStatus
                    .OrderByDescending(sos => sos.CreatedAt)
                    .FirstOrDefault()?.Name ?? "Pagado";

                // Calculamos la fecha estimada de entrega
                var minDeliveryDate = o.CreatedAt.AddDays(7);
                var maxDeliveryDate = o.CreatedAt.AddDays(14);
                string estimatedDelivery;
                
                if (currentStatus.ToLower() == "completado")
                {
                    var completedDate = o.SaleOrderStatus
                        .Where(sos => sos.Name.ToLower() == "completado")
                        .OrderByDescending(sos => sos.CreatedAt)
                        .FirstOrDefault()?.CreatedAt ?? DateTime.UtcNow;
                    
                    estimatedDelivery = $"Entregado el {completedDate:dd/MM/yyyy}";
                }
                else
                {
                    estimatedDelivery = $"{minDeliveryDate:dd}-{maxDeliveryDate:dd} de {minDeliveryDate:MMMM}";
                }

                // Obtener miniaturas de productos
                var productThumbnails = o.SaleOrderLine
                    .Take(3) // Solo las primeras 3 imágenes
                    .Select(line => new ProductThumbnailViewModel
                    {
                        ProductId = line.ProductId,
                        ImageUrl = line.Product?.ImageUrl ?? "/api/placeholder/50/50"
                    }).ToList();

                return new SaleOrderViewModel
                {
                    Id = o.Id,
                    CreatedAt = o.CreatedAt,
                    TotalPrice = o.TotalPrice,
                    CurrentStatus = currentStatus,
                    UserId = o.UserId,
                    UserName = o.User?.UserName ?? "Usuario",
                    ProductCount = o.SaleOrderLine.Count,
                    ProductThumbnails = productThumbnails,
                    EstimatedDeliveryDate = estimatedDelivery
                };
            }).ToList();

            // Aplicar filtros de estado
            if (statusFilter != "all")
            {
                orderViewModels = orderViewModels
                    .Where(o => o.CurrentStatus.ToLower() == statusFilter.ToLower())
                    .ToList();
            }

            // Aplicar ordenación
            orderViewModels = sortOrder switch
            {
                "older" => orderViewModels.OrderBy(o => o.CreatedAt).ToList(),
                "expensive" => orderViewModels.OrderByDescending(o => o.TotalPrice).ToList(),
                "cheaper" => orderViewModels.OrderBy(o => o.TotalPrice).ToList(),
                _ => orderViewModels.OrderByDescending(o => o.CreatedAt).ToList() // "recent" es el default
            };

            var viewModel = new OrdersListViewModel
            {
                Orders = orderViewModels,
                StatusFilter = statusFilter,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orderDetails = await _saleOrderService.GetOrderDetailsAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            // Verificar que la orden pertenece al usuario actual (o es un administrador)
            var order = await _saleOrderService.GetSaleOrderByIdAsync(id);
            if (order.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(orderDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            // Sólo permitir para Admin o Vendors (puedes personalizar esta lógica)
            if (!User.IsInRole("Admin") && !User.IsInRole("Vendor"))
            {
                return Forbid();
            }

            // Validar que el status sea uno de los permitidos
            var allowedStatuses = new[] { "Pagado", "Empacando", "En reparto", "Completado" };
            if (!allowedStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Estado no válido");
            }

            try
            {
                await _statusService.UpdateOrderStatusAsync(orderId, status);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}