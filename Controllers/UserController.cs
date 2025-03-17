using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class UserController : Controller
    {
       
            public IActionResult UserProfile() => View();
            [Authorize]
            public IActionResult EditProfile() => View();
            public IActionResult UsuariosBloqueados() => View();
        
    }
}
