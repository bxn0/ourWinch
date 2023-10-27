
using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;




[Authorize]
public class NewServiceController : Controller
{
    private readonly AppDbContext _context;

    public NewServiceController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = Newtonsoft.Json.JsonConvert.SerializeObject(bookedDates);
        return View("~/Views/Dashboard/NewService.cshtml");
    }
}
