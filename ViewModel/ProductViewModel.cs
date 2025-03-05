namespace EMIM.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public string? ImageUrl { get; set; }

        // Para manejar la carga de imágenes en la Vista
        public IFormFile? ImageFile { get; set; }
    }
}
