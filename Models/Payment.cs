using System.ComponentModel.DataAnnotations;

namespace EMIM.Models
{
    public class Payment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string PaymentId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime FechaPago { get; set; }
    }
}