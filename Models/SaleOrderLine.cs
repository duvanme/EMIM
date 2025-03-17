using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMIM.Models
{
    public class SaleOrderLine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public int SaleOrderId { get; set; }
        [ForeignKey("SaleOrderId")]
        public SaleOrder SaleOrder { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Subtotal => Quantity * UnitPrice;

    }
}
