using Microsoft.AspNetCore.Mvc;
using ourWinchSist.Models;
using System.Linq;

public class ServiceOrderController : Controller
{
    private readonly AppDbContext _context;

    public ServiceOrderController(AppDbContext context)
    {
        _context = context;
    }

    // GET: ServiceOrder/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ServiceOrder/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceOrder serviceOrder)
    {
        if (ModelState.IsValid)
        {
            _context.ServiceOrders.Add(serviceOrder);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); // Index metodu oluşturulmalıdır.
        }
        return View(serviceOrder);
    }

    // GET: ServiceOrder/Index
    public IActionResult Index()
    {
        return View(_context.ServiceOrders.ToList());
    }
}
