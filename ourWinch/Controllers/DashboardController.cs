using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList; // Doğru using direktifini ekleyin

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int page = 1)
    {
        var serviceOrders = _context.ServiceOrders.ToList(); // Tüm servis siparişlerini alın
        var pagedServiceOrders = serviceOrders.ToPagedList(page, 5); // Verileri sayfalayın
        return View(pagedServiceOrders);
    }
}