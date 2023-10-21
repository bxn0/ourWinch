using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class ActiveServiceController : Controller
{
    private readonly AppDbContext _context;

    public ActiveServiceController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public IActionResult SaveToActiveService(int serviceOrderId)
    {
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == serviceOrderId);
        if (serviceOrder != null)
        {
            var activeService = new ActiveService
            {
                Ordrenummer = serviceOrder.Ordrenummer,
                Produkttype = serviceOrder.Produkttype,
                Fornavn = serviceOrder.Fornavn,
                Etternavn = serviceOrder.Etternavn,
                MottattDato = serviceOrder.MottattDato,
                Feilbeskrivelse = serviceOrder.Feilbeskrivelse,           
                Status = "Process", // Bu özelliği varsayılan olarak "Process" olarak atadım.   
            };
            System.Diagnostics.Debug.WriteLine("Before Save: " + activeService.Ordrenummer);
            _context.ActiveServices.Add(activeService);
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("After Save: " + activeService.Ordrenummer);
        }

        return RedirectToAction("Index");
    }
}