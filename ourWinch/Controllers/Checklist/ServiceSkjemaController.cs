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
    public class ServiceSkjemaController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceSkjemaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: api/ServiceSkjema/5
        [HttpGet("api/ServiceSkjema/{id}")]
        public async Task<ActionResult<ServiceSkjema>> GetServiceSkjema(int id)
        {
            var mechanicals = await _context.Mechanicals.Where(m => m.ServiceOrderId == id).ToListAsync();
            var hydrolisks = await _context.Hydrolisks.Where(h => h.ServiceOrderId == id).ToListAsync();
            var electros = await _context.Electros.Where(e => e.ServiceOrderId == id).ToListAsync();
            var funksjonsTests = await _context.FunksjonsTests.Where(f => f.ServiceOrderId == id).ToListAsync();
            var trykks = await _context.Trykks.Where(t => t.ServiceOrderId == id).ToListAsync();
            var serviceOrders = await _context.ServiceOrders.Where(t => t.ServiceOrderId == id).ToListAsync();

            if (!mechanicals.Any() && !hydrolisks.Any() && !electros.Any() && !funksjonsTests.Any() && !trykks.Any())
            {
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

            return serviceSkjema;
        }
    }
}
