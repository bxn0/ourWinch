using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Dashboard;
using System.Linq;

namespace ourWinch.Controllers
{
    /// <summary>
    /// Controller for managing and displaying various types of services.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ServiceTypeController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeController" /> class.
        /// Injects the AppDbContext class for database operations.
        /// </summary>
        /// <param name="context">The database context to be used by this controller.</param>
        public ServiceTypeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the dashboard with counts of different types of service orders.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult" /> that renders the service type dashboard view.
        /// </returns>
        public IActionResult Dashboard()
        {
            // Fetch and display counts of different types of service orders from the database.
            ViewBag.GarantiCount = _context.ServiceOrders.Count(so => so.Garanti);
            ViewBag.ServiceCount = _context.ServiceOrders.Count(so => so.Servis);
            ViewBag.ReperasjonCount = _context.ServiceOrders.Count(so => so.Reperasjon);

            // Return the view for the service type dashboard.
            return View("~/Views/Dashboard/ServiceType.cshtml");
        }
    }
}
