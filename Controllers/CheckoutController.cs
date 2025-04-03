using EMIM.DTOs;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;
using Stripe;
using Stripe.Checkout;

namespace EMIM.Controllers
{
    public class CheckoutController : Controller
    {
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
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                        },
                        UnitAmount = item.Price * 100, // Convert to cents
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "checkout/success",
                CancelUrl = domain + "checkout/cancel",
            };

            var client = new StripeClient("sk_test_51R8p3nEHOgKBiXCdnckkQN5RvtEGJNlI3S6lshmlyHtGj7RMf0bfuyUq9H0GWOr5ZhHqCeD2Xs1cReK7trgiFvnA00gaCobbE6");
            var service = new SessionService(client);
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
