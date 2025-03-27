using System.ComponentModel.DataAnnotations;
using EMIM.Models;

namespace EMIM.ViewModel
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? StoreProfilePicture { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        // Método para proporcionar una imagen predeterminada si no hay foto de perfil
        public string GetProfilePictureUrl()
        {
            // Si no hay imagen, devolver una imagen predeterminada
            if (string.IsNullOrEmpty(StoreProfilePicture))
            {
                return "~/images/default-store-profile.png";
            }

            // Asegurarse de que la ruta comience con ~/
            return StoreProfilePicture.StartsWith("~/")
                ? StoreProfilePicture
                : $"~/{StoreProfilePicture.TrimStart('/')}";
        }
    }
}