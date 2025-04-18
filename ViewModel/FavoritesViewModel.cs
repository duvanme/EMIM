namespace EMIM.ViewModel
{
    public class FavoritesViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public int Count { get; set; }
    }
}