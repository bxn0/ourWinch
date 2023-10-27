using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ourWinch.Controllers.Checklist
{



    [Authorize]
    public class ServiceSkjemaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
