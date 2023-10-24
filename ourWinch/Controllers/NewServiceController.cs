
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;


public class NewServiceController : Controller
{
    private readonly AppDbContext _context;

    public NewServiceController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = Newtonsoft.Json.JsonConvert.SerializeObject(bookedDates);
        return View();
    }
}
