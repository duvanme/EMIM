using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace EMIM.ViewComponents
{
    public class CarouselCardViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CarouselCardViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetHighlightedProductsAsync();
            var productVMs = products.Select(p => new HighlightedProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();

            ViewData["TotalSlides"] = productVMs.Count;

            return View(productVMs);
        }
    }
}
