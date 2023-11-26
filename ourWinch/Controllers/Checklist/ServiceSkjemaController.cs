using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Account;
using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    [Authorize]

    /// <summary>
    /// Controller responsible for handling service schema operations.
    /// Authorization is required to access its methods.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ServiceSkjemaController : Controller
    {
        /// <summary>
        /// The database context used for data operations.
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// The logger used to log information or errors.
        /// </summary>
        private readonly ILogger<ServiceSkjemaController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceSkjemaController" /> with the specified database context and logger.
        /// </summary>
        /// <param name="context">The database context to be used for data operations.</param>
        /// <param name="logger">The logger for logging information or errors.</param>
        public ServiceSkjemaController(AppDbContext context, ILogger<ServiceSkjemaController> logger)
        {
            _context = context;
            _logger = logger; 
        }

        /// <summary>
        /// Displays the default view for the Service Schema index page.
        /// </summary>
        /// <returns>
        /// The Index view.
        /// </returns>
        public IActionResult Index()
        {
            // Return the default Index view.

            return View();
        }

        /// <summary>
        /// Retrieves the service schema for a specific service order ID.
        /// </summary>
        /// <param name="id">The ID of the service order for which the service schema is requested.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an ActionResult.
        /// If any related data is found, it returns the corresponding service schema with the associated entities.
        /// If no related data is found, it returns NotFound.
        /// </returns>
        // GET: api/ServiceSkjema/5
        [HttpGet("api/ServiceSkjema/{id}")]
        public async Task<ActionResult<ServiceSkjema>> GetServiceSkjema(int id)
        {
            _logger.LogInformation("GetServiceSkjema call, ID: {ID}", id);

            var mechanicals = await _context.Mechanicals.Where(m => m.ServiceOrderId == id).ToListAsync();
            var hydrolisks = await _context.Hydrolisks.Where(h => h.ServiceOrderId == id).ToListAsync();
            var electros = await _context.Electros.Where(e => e.ServiceOrderId == id).ToListAsync();
            var funksjonsTests = await _context.FunksjonsTests.Where(f => f.ServiceOrderId == id).ToListAsync();
            var trykks = await _context.Trykks.Where(t => t.ServiceOrderId == id).ToListAsync();
            var serviceOrders = await _context.ServiceOrders.Where(t => t.ServiceOrderId == id).ToListAsync();

            if (!mechanicals.Any() && !hydrolisks.Any() && !electros.Any() && !funksjonsTests.Any() && !trykks.Any())
            {
                _logger.LogWarning("ServiceSkjema not found, ID: {ID}", id);
                return NotFound();
            }

            var serviceSkjema = new ServiceSkjema
            {
                Mechanicals = mechanicals,
                Hydrolisks = hydrolisks,
                Electros = electros,
                FunksjonsTests = funksjonsTests,
                Trykks = trykks,
                ServiceOrders = serviceOrders
            };
            _logger.LogInformation("ServiceSkjema found and returned, ID: {ID}", id);
            return serviceSkjema;
        }
    }
}
