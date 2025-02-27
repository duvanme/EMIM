using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First name is required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40,MinimumLength = 12, ErrorMessage = "The {0} must be at {2} and at max {1}")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage ="Password does not match")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Conmfirmed password is required characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public required string ConfirmPassword { get; set; }

    }
}
