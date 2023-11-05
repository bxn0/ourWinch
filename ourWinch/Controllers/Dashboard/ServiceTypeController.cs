using Microsoft.AspNetCore.Mvc;

using ourWinch.Models.Dashboard;
using System.Linq;

namespace ourWinch.Controllers
{
    public class ServiceTypeController : Controller
    {
        private readonly AppDbContext _context;

        // AppDbContext sınıfınızı dependency injection ile enjekte edin.
        public ServiceTypeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // Veritabanından verileri çek
            ViewBag.GarantiCount = _context.ServiceOrders.Count(so => so.Garanti);
            ViewBag.ServiceCount = _context.ServiceOrders.Count(so => so.Servis);
            ViewBag.ReperasjonCount = _context.ServiceOrders.Count(so => so.Reperasjon);

            return View("~/Views/Dashboard/ServiceType.cshtml");
        }
    }
}
