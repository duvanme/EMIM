using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string ImageUrl { get; set; }
        public string StoreName { get; set; }
    }
}