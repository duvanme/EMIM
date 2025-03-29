using System.Collections.Generic;

namespace EMIM.ViewModels
{
    public class ProductCardViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<string> Roles { get; set; }
        public string CurrentUserId { get; set; }
        public int? CurrentStoreId { get; set; }
    }
}