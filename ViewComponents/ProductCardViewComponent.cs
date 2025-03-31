using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
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

    public async Task<IViewComponentResult> InvokeAsync(
    List<ProductViewModel> products = null, 
    int? storeId = null, 
    string currentUserId = null)
{
    if (products == null)
    {
        if (storeId.HasValue)
        {
            products = await _productService.GetProductsByStoreIdAsync(storeId.Value);
        }
        else
        {
            var allProducts = await _productService.GetAllProductsAsync();
            products = allProducts.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                StoreId = p.StoreId
            }).ToList();
        }
    }

    var user = await _userManager.GetUserAsync(HttpContext.User);
    var roles = user != null ? await _userManager.GetRolesAsync(user) : new List<string>();

    var model = new ProductCardViewModel
    {
        Products = products,
        Roles = roles.ToList(),
        CurrentUserId = user?.Id,
        CurrentStoreId = storeId
    };

    return View(model);
}
}
