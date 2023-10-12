// Controllers/DashboardController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var serviceOrders = _context.ServiceOrders.ToList();
        return View(serviceOrders);
    }
}
