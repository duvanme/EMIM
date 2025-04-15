// SaleOrderViewModel.cs
using EMIM.Models;
using System;
using System.Collections.Generic;

namespace EMIM.ViewModel
{
    public class SaleOrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FormattedDate => CreatedAt.ToString("dd/MM/yyyy");
        public decimal TotalPrice { get; set; }
        public string CurrentStatus { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductCount { get; set; }
        public List<ProductThumbnailViewModel> ProductThumbnails { get; set; } = new List<ProductThumbnailViewModel>();
        public string EstimatedDeliveryDate { get; set; }
    }
}