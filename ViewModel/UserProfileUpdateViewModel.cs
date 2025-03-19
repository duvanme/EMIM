using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class UserProfileUpdateViewModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string ImageProfile { get; set; } = string.Empty;
    }
}