using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Linq;

/// <summary>
/// Controller for managing customer-related operations.
/// </summary>
[Authorize]
public class CustomersController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersController" /> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays the dashboard with a paginated list of service orders.
    /// </summary>
    /// <param name="query">The search query for filtering service orders. Default is empty, meaning no filtering.</param>
    /// <param name="filter">The type of filter to apply, e.g., 'name'. Default is 'name'.</param>
    /// <param name="page">The current page number for pagination. Defaults to 1.</param>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the customers dashboard view with the filtered and paginated service orders.
    /// </returns>
    [HttpGet]
    public IActionResult Dashboard(string query = "", string filter = "name", int page = 1)
    {
        int pageSize = 10;

        // Create a query for service orders.
        var serviceOrdersQuery = _context.ServiceOrders.AsQueryable();

        // Apply filtering based on the provided query and filter type.
        if (!string.IsNullOrEmpty(query))
        {
            switch (filter)
            {
                case "name":
                    serviceOrdersQuery = serviceOrdersQuery.Where(s => s.Fornavn.Contains(query) || s.Etternavn.Contains(query));
                    break;
                    
            }
        }

        // Fetch a unique set of service orders based on the filtering and apply pagination.
        var uniqueServiceOrders = serviceOrdersQuery
            .GroupBy(s => new { s.Fornavn, s.Etternavn, s.MobilNo })
            .Select(g => g.First())
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Calculate and set total pages and current page for pagination.
        ViewBag.TotalPages = Math.Ceiling((double)serviceOrdersQuery.Count() / pageSize);
        ViewBag.CurrentPage = page;

        // Return the view for the customers dashboard, passing the list of service orders as a model.
        return View("~/Views/Dashboard/Customers.cshtml", uniqueServiceOrders);
    }

}
