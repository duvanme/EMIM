using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class CreateStoreViewModel
    {
        public int? StoreId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile StoreProfilePicture { get; set; }
    }
}
