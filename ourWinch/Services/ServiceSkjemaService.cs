using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard; // Bu satırı projenize göre uygun şekilde güncelleyin.
using System.Threading.Tasks;

namespace ourWinch.Services
{
    public class ServiceSkjemaService
    {
        private readonly AppDbContext _context;

        public ServiceSkjemaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            var isElectroCompleted = await _context.Electros.AnyAsync(e => e.ServiceOrderId == serviceOrderId && e.OK);
            var isFunksjonCompleted = await _context.FunksjonsTests.AnyAsync(f => f.ServiceOrderId == serviceOrderId && f.OK);
            var isHydroliskCompleted = await _context.Hydrolisks.AnyAsync(h => h.ServiceOrderId == serviceOrderId && h.OK);
            var isMechanicalCompleted = await _context.Mechanicals.AnyAsync(m => m.ServiceOrderId == serviceOrderId && m.OK);
            var isTrykkCompleted = await _context.Trykks.AnyAsync(t => t.ServiceOrderId == serviceOrderId && t.OK);

            Console.WriteLine($"isElectroCompleted: {isElectroCompleted}");
            Console.WriteLine($"isFunksjonCompleted: {isFunksjonCompleted}");
            Console.WriteLine($"isHydroliskCompleted: {isHydroliskCompleted}");
            Console.WriteLine($"isMechanicalCompleted: {isMechanicalCompleted}");
            Console.WriteLine($"isTrykkCompleted: {isTrykkCompleted}");

            if (isElectroCompleted && isFunksjonCompleted && isHydroliskCompleted && isMechanicalCompleted && isTrykkCompleted)
            {
                var serviceOrder = await _context.ServiceOrders.FirstOrDefaultAsync(s => s.ServiceOrderId == serviceOrderId);
                if (serviceOrder != null)
                {
                    serviceOrder.ServiceSkjema = "Ja";
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Console.WriteLine("Service order not found!");
            }
        }
    }
}
