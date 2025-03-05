using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 12, ErrorMessage = "The {0} must be at {2} and at max {1}")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "New password")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Conmfirmed password is required characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public required string ConfirmNewPassword { get; set; }

    }
}
