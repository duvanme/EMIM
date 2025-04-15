// SaleOrderStatusViewModel.cs
namespace EMIM.ViewModel
{
    public class SaleOrderStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }
    }
}