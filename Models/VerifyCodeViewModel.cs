using System.ComponentModel.DataAnnotations;

namespace EMIM.Models;

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "El código es requerido")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }
    }

