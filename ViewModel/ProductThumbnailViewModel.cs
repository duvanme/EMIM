using EMIM.Models;
using System.ComponentModel.DataAnnotations;

namespace EMIM.ViewModel
{
    public class ProductThumbnailViewModel
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}