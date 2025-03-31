using System.Collections.Generic;

namespace EMIM.ViewModel
{
    public class ProductCardViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<string> Roles { get; set; }
        public string CurrentUserId { get; set; }
        public int? CurrentStoreId { get; set; }

        public int? CurrentCategoryId { get; set; }

    }
}