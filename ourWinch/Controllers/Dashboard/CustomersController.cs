using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Linq;

[Authorize]
public class CustomersController : Controller
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = JsonConvert.SerializeObject(bookedDates);

        // Benzersiz "navn", "Etter Navn" ve "Tlf Nummer" değerlerine sahip satırları al
        var uniqueServiceOrders = _context.ServiceOrders
            .GroupBy(s => new { s.Fornavn, s.Etternavn, s.MobilNo })
            .Select(g => g.First())
            .ToList();

        return View("~/Views/Dashboard/Customers.cshtml", uniqueServiceOrders);
    }
}
