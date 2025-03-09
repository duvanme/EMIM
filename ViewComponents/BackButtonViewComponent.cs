using Microsoft.AspNetCore.Mvc;

namespace EMIM.ViewComponents
{
    public class BackButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
