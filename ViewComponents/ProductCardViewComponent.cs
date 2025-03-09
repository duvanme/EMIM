using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


public class ProductCardViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public ProductCardViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetAllProductsAsync();
        var productVMs = products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        }).ToList();

        return View(productVMs);
    }
}
