using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class HighlightedProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "La tienda es obligatoria.")]
        public int StoreId { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

        public string? StoreName { get; set; }
    }
}
