using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }

    }
}

