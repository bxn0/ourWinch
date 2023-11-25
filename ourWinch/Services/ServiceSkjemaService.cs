using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;

namespace ourWinch.Services
{

    /// <summary>
    /// Service class to handle operations related to service orders.
    /// </summary>
    public class ServiceSkjemaService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ServiceSkjemaService> _logger;


        /// <summary>
        /// Initializes a new instance of the ServiceSkjemaService class.
        /// </summary>
        /// <param name="context">Database context to interact with the underlying database.</param>
        /// <param name="logger">Logger for logging information and activities.</param>
        public ServiceSkjemaService(AppDbContext context, ILogger<ServiceSkjemaService> logger)
        {
            _context = context;
            _logger = logger; 
        }

        /// <summary>
        /// Asynchronously checks if all service tasks associated with a given service order ID are marked as completed.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to check.</param>
        /// <returns>
        /// A task representing the asynchronous operation without a return value.
        /// </returns>
        /// <remarks>
        /// The method checks for completion of tasks across various categories including Electro, Funksjon, Hydrolisk, Mechanical, and Trykk.
        /// It logs the completion status of each category.
        /// </remarks>
        public async System.Threading.Tasks.Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {

            // Check if all required operations (Electro, Funksjon, Hydrolisk, Mechanical, Trykk) are completed.
            var isElectroCompleted = await _context.Electros.AnyAsync(e => e.ServiceOrderId == serviceOrderId && e.OK);
            var isFunksjonCompleted = await _context.FunksjonsTests.AnyAsync(f => f.ServiceOrderId == serviceOrderId && f.OK);
            var isHydroliskCompleted = await _context.Hydrolisks.AnyAsync(h => h.ServiceOrderId == serviceOrderId && h.OK);
            var isMechanicalCompleted = await _context.Mechanicals.AnyAsync(m => m.ServiceOrderId == serviceOrderId && m.OK);
            var isTrykkCompleted = await _context.Trykks.AnyAsync(t => t.ServiceOrderId == serviceOrderId && t.OK);

            // Log the completion status of each category.
            _logger.LogInformation("isElectroCompleted: {Status}", isElectroCompleted);
            _logger.LogInformation("isFunksjonCompleted: {Status}", isFunksjonCompleted);
            _logger.LogInformation("isHydroliskCompleted: {Status}", isHydroliskCompleted);
            _logger.LogInformation("isMechanicalCompleted: {Status}", isMechanicalCompleted);
            _logger.LogInformation("isTrykkCompleted: {Status}", isTrykkCompleted);


            // If all checks are completed, update the service order.
            if (isElectroCompleted && isFunksjonCompleted && isHydroliskCompleted && isMechanicalCompleted && isTrykkCompleted)
            {
                var serviceOrder = await _context.ServiceOrders.FirstOrDefaultAsync(s => s.ServiceOrderId == serviceOrderId);
                if (serviceOrder != null)

                    _logger.LogInformation("Alle kontrolloperasjoner er gjennomført, ServiceOrder er oppdatert.");
                {
                    serviceOrder.ServiceSkjema = "Ja";
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                _logger.LogInformation("Service order not found!");
            }
        }
    }
}
