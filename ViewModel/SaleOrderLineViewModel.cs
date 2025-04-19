// SaleOrderLineViewModel.cs
namespace EMIM.ViewModel
{
    public class SaleOrderLineViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string StoreName { get; set; }
    }
}