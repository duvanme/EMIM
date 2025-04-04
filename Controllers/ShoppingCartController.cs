using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Identity;

public class ShoppingCartController : Controller
{
    private readonly UserManager<User> _userManager;

    public ShoppingCartController(UserManager<User> userManager)
{
    _userManager = userManager;
}


    public async Task<IActionResult> ShoppingCart()
    {
        var user = User.Identity.IsAuthenticated ? await _userManager.GetUserAsync(User) : null;
        
        var viewModel = new UserRoleViewModel
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            UserName = user?.UserName ?? "anonymous",
            Roles = user != null ? (await _userManager.GetRolesAsync(user)).ToList() : new List<string> { "default" }
        };

        return View(viewModel);
    }
}
