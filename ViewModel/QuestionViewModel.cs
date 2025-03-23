using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; } // Para mostrar el nombre del producto

        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; } // Para mostrar el nombre del usuario

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }

        public string? Answer { get; set; }

        public string Status { get; set; } // Para mostrar si está "Pending" o "Answered"
    }
}
