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
    int? categoryId = null,
    int? storeId = null,
    string query = null,
    int page = 1,
    int pageSize = 5)
    {
        List<ProductViewModel> allProducts;

        if (categoryId.HasValue)
        {
            allProducts = await _productService.GetProductsByCategoryAsync(categoryId.Value);
        }
        else if (storeId.HasValue)
        {
            allProducts = await _productService.GetProductsByStoreIdAsync(storeId.Value);
        }
        else
        {
            var Products = await _productService.GetAllProductsAsync();
            allProducts = Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoryId = p.CategoryId,
                StoreId = p.StoreId,
                ImageUrl = p.ImageUrl,
                StoreName = p.Store?.Name ?? "Tienda Desconocida" // Añade esta línea
            }).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var lowerQuery = query.ToLower();
            allProducts = allProducts
                .Where(p => p.Name.ToLower().Contains(lowerQuery) || p.StoreName.ToLower().Contains(lowerQuery))
                .ToList();
        }

        var totalItems = allProducts.Count;
        var paginatedProducts = allProducts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();



        var user = await _userManager.GetUserAsync(HttpContext.User);
        var roles = user != null ? await _userManager.GetRolesAsync(user) : new List<string>();

        var model = new ProductCardViewModel
        {
            Products = paginatedProducts,
            Roles = roles.ToList(),
            CurrentUserId = user?.Id,
            CurrentStoreId = storeId,
            CurrentCategoryId = categoryId,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };

        return View(model);
    }
}

