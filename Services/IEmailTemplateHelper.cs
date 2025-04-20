using EMIM.DTOs;

namespace EMIM.Services
{
    public interface IEmailTemplateHelper
    {
       string BuildOrderEmailBody(List<CartItem> cartItems, decimal total);
    }
}