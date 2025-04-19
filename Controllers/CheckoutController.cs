using EMIM.Data;
using EMIM.DTOs;
using EMIM.Models;
using EMIM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using System.Text.Json;
using EMIM.Services;


namespace EMIM.Controllers
{

    public class CheckoutController : Controller
    {
        private readonly EmimContext _context;
        private readonly ICheckoutService _checkoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;


        public CheckoutController(EmimContext context, ICheckoutService checkoutService, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IUserService userService)
        {
            _context = context;
            _checkoutService = checkoutService;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _userService = userService;
        }


        [HttpPost]
        public IActionResult CreateCheckoutSession([FromBody] CartRequest cartRequest)
        {
            if (cartRequest?.CartData == null || !cartRequest.CartData.Any())
            {
                return BadRequest("No cart data provided.");
            }

            var domain = "http://localhost:5136/";

            // Guardar los datos del carrito en la sesión en formato JSON
            HttpContext.Session.SetString("CartData", System.Text.Json.JsonSerializer.Serialize(cartRequest.CartData));

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
                LineItems = cartRequest.CartData.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "cop",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                        },
                        UnitAmount = (long)(item.Price * 100), // Convert to cents
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "Checkout/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "Checkout/Cancel",
            };

            var client = new StripeClient("sk_test_51R9zjIPIgHxDR0Zx1u2Agfq12RExCc1OLW6T108WLLjqO1DbLjRi1T6cE5wwbmda1rwBqbyHzZzpw9t46MKOlDjk00ODqU6bJ3");
            var service = new SessionService(client);
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }

        public async Task<IActionResult> Success(string session_id)
        {
            var client = new StripeClient("sk_test_51R9zjIPIgHxDR0Zx1u2Agfq12RExCc1OLW6T108WLLjqO1DbLjRi1T6cE5wwbmda1rwBqbyHzZzpw9t46MKOlDjk00ODqU6bJ3");
            var service = new SessionService(client);
            var session = await service.GetAsync(session_id);

            if (session.PaymentStatus == "paid")
            {
                string userEmail = session.CustomerDetails.Email;
                long totalAmount = session.AmountTotal ?? 0;
                string paymentId = session.Id;
                string paymentStatus = session.PaymentStatus;

                // Guardar en la tabla Payment
                await SavePaymentInDB(userEmail, totalAmount / 100, paymentId, paymentStatus);

                // Obtener los datos del carrito desde la sesión
                var cartDataJson = HttpContext.Session.GetString("CartData");
                if (!string.IsNullOrEmpty(cartDataJson))
                {
                    var cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartDataJson);

                    // Determinar el ID del usuario
                    string userId;
                    if (User.Identity.IsAuthenticated)
                    {
                        userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    }
                    else
                    {
                        // Si no está autenticado, buscar el usuario por email
                        var user = await _context.Users
                            .Where(u => u.Email == userEmail)
                            .FirstOrDefaultAsync();

                        if (user != null)
                        {
                            userId = user.Id;
                        }
                        else
                        {
                            // Para una compra de invitado, usamos una identificación genérica
                            userId = "guest-" + Guid.NewGuid().ToString();
                        }
                    }

                    try
                    {
                        // Procesar la orden con el ID de usuario determinado
                        await _checkoutService.ProcessSuccessfulPaymentAsync(userId, cartItems, totalAmount / 100);

                        // Limpiar los datos del carrito de la sesión
                        HttpContext.Session.Remove("CartData");

                        ViewBag.ClearCart = true;
                        ViewBag.Message = "Pago realizado con éxito.";

                        var user = await _userService.FindUserByIdAsync(userId); // Suponiendo que tienes un servicio para obtener al usuario
                        var Email = user.Email; // Obtienes el correo registrado
                        var emailBody = EmailTemplateHelper.BuildOrderEmailBody(cartItems, totalAmount / 100);

                        await _emailService.SendEmailAsync(
                            Email,
                            "Detalles de tu compra en EMIM",
                            emailBody);
                    }
                    catch (Exception ex)
                    {
                        // Registrar el error y mostrar un mensaje al usuario
                        Console.WriteLine($"Error al procesar la orden: {ex.Message}");
                        ViewBag.Message = "El pago se realizó, pero hubo un problema al procesar tu orden.";
                    }
                }
                else
                {
                    ViewBag.Message = "Pago realizado, pero no se encontraron datos del carrito.";
                }
            }
            else
            {
                ViewBag.Message = "El pago no se completó.";
            }
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
        private async Task SavePaymentInDB(string email, decimal total, string paymentId, string status)
        {
            var payment = new Payment
            {
                Email = email,
                Total = total,
                PaymentId = paymentId,
                Status = status,
                FechaPago = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            Console.WriteLine("Pago guardado correctamente.");
        }


        private async Task<List<CartItem>> GetCartFromLocalStorage()
        {
            // Recuperar los datos del carrito de la sesión
            var cartJson = HttpContext.Session.GetString("CartData");
            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
            }
            return new List<CartItem>();
        }
    }
}
