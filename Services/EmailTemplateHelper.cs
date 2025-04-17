using EMIM.DTOs;

namespace EMIM.Services
{
    public static class EmailTemplateHelper
    {
        public static string BuildOrderEmailBody(List<CartItem> cartItems, decimal total)
        {
            var body = "<h2>¡Gracias por tu compra en EMIM!</h2>";
            body += "<p>Aquí están los detalles de tu pedido:</p>";
            body += "<table border='1' cellpadding='10'><tr><th>Producto</th><th>Cantidad</th><th>Precio</th></tr>";

            foreach (var item in cartItems)
            {
                body += $"<tr><td>{item.Name}</td><td>{item.Quantity}</td><td>${item.Price:N0}</td></tr>";
            }

            body += $"</table><br/><h3>Total: ${total:N0}</h3>";
            return body;
        }
    }
}
