using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace EMIM.Models
{
    public class SaleOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        [Required]
        public decimal TotalPrice { get; set; }
        public ICollection<SaleOrderLine> SaleOrderLine { get; set; } = new List<SaleOrderLine>();
        public ICollection<SaleOrderStatus> SaleOrderStatus { get; set; } = new List<SaleOrderStatus>();
    }
}
