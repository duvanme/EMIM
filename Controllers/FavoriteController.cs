using EMIM.Models;
using EMIM.Views.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Controllers{

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly UserManager<User> _userManager;
    
    public FavoritesController(IFavoriteService favoriteService, UserManager<User> userManager)
    {
        _favoriteService = favoriteService;
        _userManager = userManager;
    }
    
    [HttpPost("add/{productId}")]
    [Authorize]
    public async Task<IActionResult> AddFavorite(int productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var result = await _favoriteService.AddFavoriteAsync(userId, productId);
        if (!result)
            return BadRequest("No se pudo agregar a favoritos");
            
        return Ok(new { message = "Producto agregado a favoritos" });
    }
    
    [HttpDelete("remove/{productId}")]
    [Authorize]
    public async Task<IActionResult> RemoveFavorite(int productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var result = await _favoriteService.RemoveFavoriteAsync(userId, productId);
        if (!result)
            return BadRequest("No se pudo quitar de favoritos");
            
        return Ok(new { message = "Producto eliminado de favoritos" });
    }
    
    [HttpGet("check/{productId}")]
    [Authorize]
    public async Task<IActionResult> CheckFavorite(int productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var isFavorite = await _favoriteService.IsFavoriteAsync(userId, productId);
        return Ok(new { isFavorite });
    }
}
}