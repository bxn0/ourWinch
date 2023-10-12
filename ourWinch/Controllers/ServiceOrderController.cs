using Microsoft.AspNetCore.Mvc;
using ourWinchSist.Models;
using System.Linq;

public class ServiceOrderController : Controller
{
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
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.Id == id);
        if (serviceOrder == null)
        {
            return NotFound();
        }
        List<ServiceOrder> serviceOrderList = new List<ServiceOrder> { serviceOrder };
        return View("Checklist", serviceOrderList);  // Burada "Checklist" view'ına List<ServiceOrder> modelini gönderiyoruz.
    }

}