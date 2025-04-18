using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class OrdersListViewModel
    {
        public List<SaleOrderViewModel> Orders { get; set; } = new List<SaleOrderViewModel>();
        public string StatusFilter { get; set; } = "all";
        public string SortOrder { get; set; } = "recent";
    }
}