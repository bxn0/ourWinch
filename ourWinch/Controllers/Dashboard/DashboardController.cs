using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


/// <summary>
/// Controller for handling dashboard operations in the application.
/// </summary>
[Authorize]
public class DashboardController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Constant defining the number of items per page for pagination.
    /// </summary>
    private const int PageSize = 7;


    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardController" /> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays the dashboard with a paginated list of active service orders.
    /// </summary>
    /// <param name="page">The current page number for pagination. Defaults to 1 if not specified.</param>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the dashboard view with the paginated list of service orders.
    /// </returns>
    [HttpGet]
    public IActionResult Index(int page = 1)
    {
        // Check and display success messages if available.
        if (TempData["SuccessMessage"] != null)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
        }

        var message = TempData["LoginSuccessMessage"] as string;

        // Display login success message if it exists.
        if (!string.IsNullOrEmpty(message))
        {
            ViewBag.LoginSuccessMessage = message;
        }

        // Calculate the total number of items and pages for pagination.
        var totalItems = _context.ServiceOrders.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        page = Math.Clamp(page, 1, totalPages);

        // Fetch the paginated list of service orders from the database.
        var serviceOrders = _context.ServiceOrders
            .OrderByDescending(cs => cs.MottattDato)
        .Skip((page - 1) * PageSize)
        .Take(PageSize)
        .ToList();

        // Set current and total page numbers in ViewBag for use in the view.
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        // Return the view for the dashboard, passing the list of service orders as a model.
        return View("~/Views/Dashboard/ActiveService.cshtml", serviceOrders);

    }


}