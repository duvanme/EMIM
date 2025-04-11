using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class EditStoreViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }

        public string? StoreProfilePicturePath { get; set; } // Ruta que se va a mantener si no se sube nueva
        public IFormFile? StoreProfilePicture { get; set; } // La imagen nueva que se va a subir
        public string? ExistingProfilePicturePath { get; set; } // Ruta existente (para mostrarla en la vista)

    }


}