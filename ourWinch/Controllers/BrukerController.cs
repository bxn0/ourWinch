using Microsoft.AspNetCore.Mvc;
using ourWinch.Models;
using ourWinch.Utility;


namespace ourWinch.Controllers
{
    public class BrukerController : Controller
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public BrukerController(ApplicationDBContext context)
        {
            _applicationDBContext = context;
        }

        public IActionResult Index()
        {
            List<Bruker> objBrukerList = _applicationDBContext.Bruker.ToList();
            return View(objBrukerList);
        }
    }
}
