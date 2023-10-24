using OurWinch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList; // Doğru using direktifini ekleyin

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? page)
    {
        int pageNumber = (page ?? 1); // Sayfa numarasını veya varsayılan olarak 1'i alın
        int pageSize = 5; // Sayfa başına öğe sayısı

        var serviceOrders = _context.ServiceOrders.ToPagedList(pageNumber, pageSize); // Verileri sayfalayın
        return View(serviceOrders);
    }


}
