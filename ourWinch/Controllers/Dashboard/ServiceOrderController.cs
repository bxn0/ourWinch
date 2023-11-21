using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Dashboard;
using System.Linq;




[Authorize]
public class ServiceOrderController : Controller
{
    private readonly AppDbContext _context;

    public ServiceOrderController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = Newtonsoft.Json.JsonConvert.SerializeObject(bookedDates);
        return View("~/Views/Dashboard/NewService.cshtml");
    }

    public IActionResult NewService()
    {
        // Retrieve the highest ServiceOrderId from the database and increment it to get the new order number
        var lastOrder = _context.ServiceOrders.OrderByDescending(u => u.Ordrenummer).FirstOrDefault();
        var newOrderNumber = (lastOrder != null) ? lastOrder.Ordrenummer + 1 : 230136;


        // Create a new ServiceOrder object with the new order number
        ServiceOrder model = new ServiceOrder
        {
            Ordrenummer = newOrderNumber // Make sure this property exists and is the correct one
            // Initialize other necessary properties, if any
        };

        // Pass the model to the view
        return View("~/Views/Dashboard/NewService.cshtml", model);
    }
    // POST: ServiceOrder/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceOrder serviceOrder)
    {
       var lastOrder = _context.ServiceOrders.OrderByDescending(o => o.Ordrenummer).FirstOrDefault();
        var newOrderNumber = (lastOrder != null) ? lastOrder.Ordrenummer + 1 : 230136;

        serviceOrder.Ordrenummer = newOrderNumber;
        serviceOrder.Status = "Process";
        TempData["SuccessMessage"] = "Bestillingen er vellykket registrert! Bestillingsnummer: " + newOrderNumber;
        _context.ServiceOrders.Add(serviceOrder);
        _context.SaveChanges();

        // Save to ActiveService
        var serviceManager = new ServiceManager(_context);
        serviceManager.SaveToActiveService(serviceOrder.ServiceOrderId);

        return RedirectToAction("Index", "Dashboard");
    }

    //Change "Status"
    [HttpPost]
    public IActionResult UpdateStatus(int id, string newStatus)
    {
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
        if (serviceOrder != null)
        {
            serviceOrder.Status = newStatus;
            _context.SaveChanges();
            return Ok(); // Başarılı güncelleme yanıtı
        }
        return NotFound(); // Servis bulunamazsa hata yanıtı
    }


    // GET: ServiceOrder/index
    public IActionResult Index()
    {
        return View(_context.ServiceOrders.ToList());
    }

    // GET: ServiceOrder/Details
    public IActionResult Details(int id, string category = "Mechanical")
    {
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
        if (serviceOrder == null)
        {
            return NotFound();
        }
        List<ServiceOrder> serviceOrderList = new List<ServiceOrder> { serviceOrder };

        switch (category)
        {
            case "Mechanical":
                return View("~/Views/Mechanical/create.cshtml", serviceOrderList);
            case "Hydrolisk":
                return View("~/Views/Hydrolisk/create.cshtml", serviceOrderList);
            case "Electro":
                return View("~/Views/Electro/create.cshtml", serviceOrderList);
            case "FunksjonsTest":
                return View("~/Views/FunksjonsTest/create.cshtml", serviceOrderList);
            case "Trykk":
                return View("~/Views/Trykk/create.cshtml", serviceOrderList);
            // Diğer kategoriler için de benzer case blokları ekleyebilirsiniz.
            default:
                return NotFound();
        }
    }
}