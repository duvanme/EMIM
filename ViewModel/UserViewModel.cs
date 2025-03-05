using System.ComponentModel.DataAnnotations;
using EMIM.Models;

namespace EMIM.ViewModel{

    //Modelo de vista para el registro de usuarios obligatorios
    public class UserViewModel {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; } 

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1}")]
        [DataType(DataType.Password)]        
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}