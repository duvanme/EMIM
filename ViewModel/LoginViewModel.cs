using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        [Display(Name= "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
