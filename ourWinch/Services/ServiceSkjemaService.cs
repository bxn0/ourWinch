using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;

namespace ourWinch.Services
{
    public class ServiceSkjemaService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceSkjemaService> _logger;
        public ServiceSkjemaService(AppDbContext context, ILogger<ServiceSkjemaService> logger)
        {
            _context = context;
            _logger = logger; // Logger burada tanımlandı
        }


        public async System.Threading.Tasks.Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            var isElectroCompleted = await _context.Electros.AnyAsync(e => e.ServiceOrderId == serviceOrderId && e.OK);
            var isFunksjonCompleted = await _context.FunksjonsTests.AnyAsync(f => f.ServiceOrderId == serviceOrderId && f.OK);
            var isHydroliskCompleted = await _context.Hydrolisks.AnyAsync(h => h.ServiceOrderId == serviceOrderId && h.OK);
            var isMechanicalCompleted = await _context.Mechanicals.AnyAsync(m => m.ServiceOrderId == serviceOrderId && m.OK);
            var isTrykkCompleted = await _context.Trykks.AnyAsync(t => t.ServiceOrderId == serviceOrderId && t.OK);

            _logger.LogInformation("isElectroCompleted: {Status}", isElectroCompleted);
            _logger.LogInformation("isFunksjonCompleted: {Status}", isFunksjonCompleted);
            _logger.LogInformation("isHydroliskCompleted: {Status}", isHydroliskCompleted);
            _logger.LogInformation("isMechanicalCompleted: {Status}", isMechanicalCompleted);
            _logger.LogInformation("isTrykkCompleted: {Status}", isTrykkCompleted);

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
