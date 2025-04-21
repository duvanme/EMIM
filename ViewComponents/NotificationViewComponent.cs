using Microsoft.AspNetCore.Mvc;
using EMIM.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace EMIM.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly EmimContext _context;

        public NotificationViewComponent(EmimContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Obtener el número de tiendas con estado "pending"
            int pendingStoresCount = await _context.Stores
                .Where(s => s.StoreStatus == "pending")
                .CountAsync();

            return View(pendingStoresCount);
        }
    }
}