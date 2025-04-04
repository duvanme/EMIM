using System.ComponentModel.DataAnnotations;
using EMIM.Models;

namespace EMIM.ViewModel{

    //Modelo de vista para el registro de usuarios obligatorios
    public class AdminViewModel {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; } 

        [Required]
        public Role Role { get; set; }
    }
}