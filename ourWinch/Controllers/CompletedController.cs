using OurWinch.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

public class CompletedController : Controller
{
    private readonly AppDbContext _context;

    public CompletedController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard(int? page)
    {
        int pageNumber = page ?? 1; // Sayfa numarasını veya varsayılan olarak 1'i alın
        int pageSize = 3; // Sayfa başına öğe sayısı

        var serviceOrders = _context.ServiceOrders.ToPagedList(pageNumber, pageSize); // Verileri sayfalayın
        return View("Completed", serviceOrders);
    }
}
