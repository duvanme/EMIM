using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class EditUserProfileViewModel
    {
        public string? FirstName { get; set;}

        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Email{ get; set; }

        public string? PhoneNumber{ get; set; }

        public IFormFile? UserProfilePicture { get; set; }

        public String? UserProfilePicturePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
