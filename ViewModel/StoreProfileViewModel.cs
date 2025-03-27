using EMIM.Models;
using EMIM.ViewModels;
using System.Collections.Generic;

namespace EMIM.ViewModel
{
    public class StoreProfileViewModel
    {
        public StoreViewModel Store { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}