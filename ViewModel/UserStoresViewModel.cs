using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class UserStoresViewModel
    {
        public string UserId { get; set; }
        public int StoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public List<CreateStoreViewModel> Stores { get; set; } = new List<CreateStoreViewModel>();
    }
}
