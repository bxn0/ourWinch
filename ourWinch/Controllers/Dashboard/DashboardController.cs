// Controllers/DashboardController.cs
using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;



[Authorize]
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
        return View("~/Views/Dashboard/ActiveService.cshtml", serviceOrders);
    }


    public IActionResult Page(int page = 1)
    {
        return View("~/Views/Dashboard/ActiveService.cshtml");
    }

}