using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




/// <summary>
/// The controller in the application for managing active services.
/// </summary>
[Authorize]
public class ActiveServiceController : Controller
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
    /// Initializes a new instance of the <see cref="ActiveServiceController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public ActiveServiceController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActiveServiceController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public static int PageSizeValue
    {
        get
        {
            return PageSize;
        }
    }

    /// <summary>
    /// Handles the HTTP GET request to retrieve a paginated list of active services.
    /// </summary>
    /// <param name="page">The current page number for pagination. Defaults to 1 if not specified.</param>
    /// <returns>
    /// An asynchronous task that results in an <see cref="IActionResult"/> which renders the active services view with the paginated list of active services.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        // Calculate the total number of items and the total number of pages needed for pagination.
        var totalItems = await _context.ActiveServices.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

        // Ensure the requested page is within the valid range.
        page = Math.Clamp(page, 1, totalPages);

        // Fetch the list of active services from the database, applying pagination.
        var activServices = await _context.ActiveServices
            .OrderByDescending(cs => cs.MottattDato)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // Store the current and total page numbers in ViewBag to use in the view.
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        // Return the view for displaying active services, passing the list of services as a model.
        return View("~/Views/Dashboard/ActiveService.cshtml", activServices);
    }

    /// <summary>
    /// Handles the HTTP DELETE request to delete an active service from the database. NOT AVAILABLE NOW. WILL BE DEVELOPED
    /// </summary>
    /// <param name="id">The ID of the active service to be deleted.</param>
    /// <returns>
    /// An asynchronous task that results in an <see cref="IActionResult"/>.
    /// Returns a 'NotFound' result if the service is not found, or 'NoContent' if the service is successfully deleted.
    /// </returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteService(int id)
    {
        // Search for the active service in the ActiveService table by its ID.
        var activeService = await _context.ActiveServices.FindAsync(id);
        if (activeService == null)
        {
            return NotFound();
        }

        // Search for the active service in the ActiveService table by its ID.
        _context.ActiveServices.Remove(activeService);
        await _context.SaveChangesAsync();

        return NoContent();
    }



}
