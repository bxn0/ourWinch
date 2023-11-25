using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

/// <summary>
/// Controller for showing to new services in the application.
/// </summary>
[Authorize]
public class NewServiceController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewServiceController" /> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public NewServiceController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays the dashboard for new services.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the dashboard view for new services.
    /// </returns>
    public IActionResult Dashboard()
    {
        // Fetch and serialize booked dates for service orders to be used in the dashboard.
        List<DateTime> bookedDates = _context.ServiceOrders.Select(s => s.MottattDato).ToList();
        ViewBag.BookedDates = Newtonsoft.Json.JsonConvert.SerializeObject(bookedDates);

        // Return the view for the new services dashboard.
        return View("~/Views/Dashboard/NewService.cshtml");
    }
}
