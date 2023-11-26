using ourWinch.Models;
using ourWinch.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


/// <summary>
/// Controller for managing completed services in the application.
/// </summary>
[Authorize]
public class CompletedServiceController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Logger for logging information, errors, and other relevant data.
    /// </summary>
    private readonly ILogger<CompletedServiceController> _logger;

    /// <summary>
    /// Constant defining the number of items per page for pagination.
    /// </summary>
    private const int PageSize = 7;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompletedServiceController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    /// <param name="logger">The logger to be used by this controller.</param>
    public CompletedServiceController(AppDbContext context, ILogger<CompletedServiceController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Handles the HTTP GET request to retrieve a paginated list of completed services.
    /// </summary>
    /// <param name="page">The current page number for pagination. Defaults to 1 if not specified.</param>
    /// <returns>
    /// An asynchronous task that results in an <see cref="IActionResult"/> which renders the completed services view with the paginated list of services.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        // Query to fetch all services with the status 'Fulfort' (Completed).
        var query = _context.ServiceOrders.Where(so => so.Status == "Fulfort");

        // Calculate the total number of items and the total number of pages needed for pagination.
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

        // Ensure the requested page is within the valid range.
        page = Math.Clamp(page, 1, totalPages);

        // Fetch the list of completed services from the database, applying pagination.
        var completedServices = await query
            .OrderByDescending(cs => cs.MottattDato)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // Store the current and total page numbers in ViewBag to use in the view.
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        // Return the view for displaying completed services, passing the list of services as a model.
        return View("~/Views/Dashboard/CompletedService.cshtml", completedServices);
    }


}
