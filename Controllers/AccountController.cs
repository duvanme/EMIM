using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult StoreProfile()
        {
            return View();
        }

    }
}
