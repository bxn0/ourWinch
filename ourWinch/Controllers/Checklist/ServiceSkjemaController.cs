using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ourWinch.Controllers.Checklist
{
    public class ServiceSkjemaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
