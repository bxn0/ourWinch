using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




[Authorize]
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
        var serviceManager = new ServiceManager(_context);
        serviceManager.SaveToActiveService(serviceOrderId);
        return RedirectToAction("Index");
    }
}