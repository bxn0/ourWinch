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

    public IActionResult NewService()
    {
        ServiceOrder model = new ServiceOrder();
        // Modelinizde özel başlangıç değerleri atamak isterseniz bu kısmı kullanabilirsiniz
        return View(model);
    }

    // POST: ServiceOrder/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceOrder serviceOrder)
    {
        var lastOrder = _context.ServiceOrders.OrderByDescending(o => o.Ordrenummer).FirstOrDefault();
        var newOrderNumber = (lastOrder != null) ? lastOrder.Ordrenummer + 1 : 230001;

        serviceOrder.Ordrenummer = newOrderNumber;

        _context.ServiceOrders.Add(serviceOrder);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }



    // GET: ServiceOrder/Index
    public IActionResult Index()
    {
        return View(_context.ServiceOrders.ToList());
    }

    // GET: ServiceOrder/Checklist
    public IActionResult Checklist()
    {
        var serviceOrders = _context.ServiceOrders.ToList();
        return View(serviceOrders);
    }


    public IActionResult Details(int id)
    {
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
        if (serviceOrder == null)
        {
            return NotFound();
        }
        List<ServiceOrder> serviceOrderList = new List<ServiceOrder> { serviceOrder };
        return View("Checklist", serviceOrderList);  // Burada "Checklist" view'ına List<ServiceOrder> modelini gönderiyoruz.
    }

}