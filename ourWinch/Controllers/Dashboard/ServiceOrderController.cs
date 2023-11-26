using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Dashboard;
using System.Linq;

/// <summary>
/// Controller for handling service order operations in the application.
/// This controller is accessible only to authorized users.
/// </summary>
[Authorize]
public class ServiceOrderController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceOrderController" /> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public ServiceOrderController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays the dashboard for service orders.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the service order dashboard view.
    /// </returns>
    public IActionResult Dashboard()
    {
        // Return the view for the service order dashboard.
        return View("~/Views/Dashboard/NewService.cshtml");
    }

    /// <summary>
    /// Creates a new service order and displays the form for entering its details.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the view for creating a new service order.
    /// </returns>
    public IActionResult NewService()
    {
        // Retrieve the highest ServiceOrderId from the database and increment it to get the new order number.
        var lastOrder = _context.ServiceOrders.OrderByDescending(u => u.Ordrenummer).FirstOrDefault();
        var newOrderNumber = (lastOrder != null) ? lastOrder.Ordrenummer + 1 : 230136;


        // Create a new ServiceOrder object with the new order number
        ServiceOrder model = new ServiceOrder
        {
            Ordrenummer = newOrderNumber 
            
        };

        // Pass the model to the view
        return View("~/Views/Dashboard/NewService.cshtml", model);
    }

    /// <summary>
    /// Processes the creation of a new service order and adds it to the database.
    /// </summary>
    /// <param name="serviceOrder">The service order object to be created and saved.</param>
    /// <returns>
    /// A redirection to the 'Index' action of the 'Dashboard' controller after successful creation of the service order.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceOrder serviceOrder)
    {
        // Generate a new order number by incrementing the last order number in the database.
        var lastOrder = _context.ServiceOrders.OrderByDescending(o => o.Ordrenummer).FirstOrDefault();
        var newOrderNumber = (lastOrder != null) ? lastOrder.Ordrenummer + 1 : 230136;

        // Set the order number and status for the new service order.
        serviceOrder.Ordrenummer = newOrderNumber;
        serviceOrder.Status = "Process";

        // Display a success message with the new order number.
        TempData["SuccessMessage"] = "Bestillingen er vellykket registrert! Bestillingsnummer: " + newOrderNumber;

        // Add the new service order to the database and save changes.
        _context.ServiceOrders.Add(serviceOrder);
        _context.SaveChanges();

        // Save to ActiveService
        var serviceManager = new ServiceManager(_context);
        serviceManager.SaveToActiveService(serviceOrder.ServiceOrderId);

        // Redirect to the dashboard index after successful creation.
        return RedirectToAction("Index", "Dashboard");
    }

    /// <summary>
    /// Updates the status of a specific service order.
    /// </summary>
    /// <param name="id">The ID of the service order to be updated.</param>
    /// <param name="newStatus">The new status to be set for the service order.</param>
    /// <returns>
    /// An 'Ok' result if the update is successful, or a 'NotFound' result if the specified service order does not exist.
    /// </returns>
    [HttpPost]
    public IActionResult UpdateStatus(int id, string newStatus)
    {
        // Retrieve the service order by its ID.
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
        if (serviceOrder != null)
        {
            // Update the status of the service order.
            serviceOrder.Status = newStatus;
            _context.SaveChanges();

            // Return an Ok response for a successful update.
            return Ok(); 
        }

        // Return a NotFound response if the service order cannot be found.
        return NotFound(); 
    }

    /// <summary>
    /// Displays a list of all service orders.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the view with the list of all service orders.
    /// </returns>
    public IActionResult Index()
    {
        // Fetch all service orders and pass them to the view.
        return View(_context.ServiceOrders.ToList());
    }

    /// <summary>
    /// Displays the details of a specific service order, categorized by its type.
    /// </summary>
    /// <param name="id">The ID of the service order to display.</param>
    /// <param name="category">The category of the service order, defaulting to 'Mechanical'.</param>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the view for the specific category of the service order.
    /// Returns 'NotFound' if the service order does not exist or the category is not recognized.
    /// </returns>
    public IActionResult Details(int id, string category = "Mechanical")
    {
        // Retrieve the service order by its ID.
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
        if (serviceOrder == null)
        {
            // Return a NotFound response if the service order cannot be found.
            return NotFound();
        }
        List<ServiceOrder> serviceOrderList = new List<ServiceOrder> { serviceOrder };

        // Render a view based on the category of the service order.
        switch (category)
        {
            case "Mechanical":
                return View("~/Views/Mechanical/create.cshtml", serviceOrderList);
            case "Hydrolisk":
                return View("~/Views/Hydrolisk/create.cshtml", serviceOrderList);
            case "Electro":
                return View("~/Views/Electro/create.cshtml", serviceOrderList);
            case "FunksjonsTest":
                return View("~/Views/FunksjonsTest/create.cshtml", serviceOrderList);
            case "Trykk":
                return View("~/Views/Trykk/create.cshtml", serviceOrderList);
            
            default:
                // Return a NotFound response if the category is not recognized.
                return NotFound();
        }
    }
}