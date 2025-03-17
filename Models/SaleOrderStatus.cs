using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMIM.Models
{
    public class SaleOrderStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public SaleOrder SaleOrder { get; set; }
    }
}
