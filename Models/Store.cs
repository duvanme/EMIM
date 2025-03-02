using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMIM.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
