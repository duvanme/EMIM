using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMIM.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        public int Quantity { get; set; }

        // Claves foráneas
        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una tienda")]
        public int StoreId { get; set; }

        // Propiedades de navegación (marcadas como `virtual` y opcionales)
        public virtual Category? Category { get; set; }
        public virtual Store? Store { get; set; }

        // 🔹 URL de la imagen guardada en el servidor
        public string? ImageUrl { get; set; }

        // 🔹 Propiedad temporal para recibir la imagen
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<SaleOrderLine> SaleOrderLine { get; set; } = new List<SaleOrderLine>();
    }

}

