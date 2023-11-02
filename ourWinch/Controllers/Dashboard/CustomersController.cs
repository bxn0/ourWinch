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

    public IActionResult Dashboard(string query = "", string filter = "name", int page = 1)
    {
        int pageSize = 10;
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = JsonConvert.SerializeObject(bookedDates);

        var serviceOrdersQuery = _context.ServiceOrders.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            switch (filter)
            {
                case "name":
                    serviceOrdersQuery = serviceOrdersQuery.Where(s => s.Fornavn.Contains(query) || s.Etternavn.Contains(query));
                    break;
                    // You can add more cases for other filters here
            }
        }

        var uniqueServiceOrders = serviceOrdersQuery
            .GroupBy(s => new { s.Fornavn, s.Etternavn, s.MobilNo })
            .Select(g => g.First())
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.TotalPages = Math.Ceiling((double)serviceOrdersQuery.Count() / pageSize);
        ViewBag.CurrentPage = page;

        return View("~/Views/Dashboard/Customers.cshtml", uniqueServiceOrders);
    }

}
