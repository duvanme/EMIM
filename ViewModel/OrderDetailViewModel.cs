
using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
     public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FormattedDate => CreatedAt.ToString("dd/MM/yyyy");
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }
        public string CurrentStatus { get; set; }
        public int CurrentStatusId { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
    }
}
