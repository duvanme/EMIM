using EMIM.Data;
using EMIM.DTOs;
using EMIM.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace EMIM.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly EmimContext _context;
        public CheckoutController(EmimContext context) // Inyecta EmimContext
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCheckoutSession([FromBody] CartRequest cartRequest)
        {

            if (cartRequest?.CartData == null || !cartRequest.CartData.Any())
            {
                return BadRequest("No cart data provided.");
            }

            var domain = "https://localhost:7268/";

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
                        UnitAmount = item.Price * 100, // Convert to cents
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

                // Guardar en la base de datos
                await SavePaymentInDB(userEmail, totalAmount / 100, paymentId, paymentStatus);
                ViewBag.Message = "Pago realizado con éxito.";
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
    }
}
