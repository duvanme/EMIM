using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El Nombre es requerido")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "El Correo es requerido")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(40,MinimumLength = 12, ErrorMessage = "The {0} must be at {2} and at max {1}")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage ="La Contraseña no coincide")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Ingresa tu dirección para poder entregar tus productos")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "La Contraseña debe de tener al menos un caracteres especiales, un número, una mayuscula y una minuscula ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}
