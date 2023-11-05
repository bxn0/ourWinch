using Microsoft.AspNetCore.Mvc;

namespace ourWinch.Controllers
{
    public class ServiceTypeController : Controller
    {
        public IActionResult Dashboard()
        {
            // Statik verileri ViewBag ile View'a gönder
            ViewBag.GarantiCount = 125;
            ViewBag.ServiceCount = 88;
            ViewBag.ReperasjonCount = 245;

            return View("~/Views/Dashboard/ServiceType.cshtml");
        }
    }
}
