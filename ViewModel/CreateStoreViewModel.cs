using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class CreateStoreViewModel
    {
        public int? StoreId { get; set; }
        [Required(ErrorMessage = "El NIT de la tienda es obligatorio")]
        public string Nit { get; set; }
        [Required(ErrorMessage = "El nombre de la tienda es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La descripcion de la tienda es obligatoria")]
        public string Description { get; set; }
        [Required(ErrorMessage = "La direccion de la tienda es obligatoria")]
        public string Location { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "La imagen de la tienda es obligatorio")]
        public IFormFile StoreProfilePicture { get; set; }
    }
}
