using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductCardViewComponent : ViewComponent
{
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;

    public ProductCardViewComponent(IProductService productService, UserManager<User> userManager)
    {
        _productService = productService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetAllProductsAsync();
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var roles = user != null ? await _userManager.GetRolesAsync(user) : new List<string>();

        var productVMs = products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        }).ToList();

        var model = new ProductCardViewModel
        {
            Products = productVMs,
            Roles = roles.ToList()
        };

        return View(model);
    }
}