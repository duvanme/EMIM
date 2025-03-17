using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMIM.Models
{
    public enum QuestionStatus
    {
        Pending,
        Answered
    }

    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Product Producto { get; set; } // Relación con la entidad Producto

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }

        public string? Answer { get; set; }

        [Required]
        public QuestionStatus Status { get; set; } = QuestionStatus.Pending;
    }
}
