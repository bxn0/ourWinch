<<<<<<< HEAD:ourWinch/Controllers/DashboardController.cs
﻿using OurWinch.Models;
=======
﻿// Controllers/DashboardController.cs
using ourWinch.Models;
using System.Collections.Generic;
>>>>>>> umit:ourWinch/Controllers/Dashboard/DashboardController.cs
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
<<<<<<< HEAD:ourWinch/Controllers/DashboardController.cs
        int pageNumber = (page ?? 1); // Sayfa numarasını veya varsayılan olarak 1'i alın
        int pageSize = 5; // Sayfa başına öğe sayısı

        var serviceOrders = _context.ServiceOrders.ToPagedList(pageNumber, pageSize); // Verileri sayfalayın
        return View(serviceOrders);
    }
}
=======
        var serviceOrders = _context.ServiceOrders.ToList();
        return View("~/Views/Dashboard/ActiveService.cshtml", serviceOrders);
    }

    public IActionResult Page(int page = 1)
    {
        return View("~/Views/Dashboard/ActiveService.cshtml");
    }

}
>>>>>>> umit:ourWinch/Controllers/Dashboard/DashboardController.cs
