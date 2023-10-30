using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Account;
using ourWinch.Models.Dashboard;

namespace ourWinch.Controllers.Checklist
{
    [Authorize]
    public class ServiceSkjemaController : Controller
    {
        private readonly AppDbContext _context; // DbContext'inizin adını güncelledim.

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
        public async Task<ActionResult<IEnumerable<ServiceOrder>>> GetServiceSkjema(int id)
        {
            var serviceOrders = await _context.ServiceOrders.Where(so => so.ServiceOrderId == id).ToListAsync();

            if (serviceOrders == null || !serviceOrders.Any())
            {
                return NotFound();
            }

            return serviceOrders;
        }

    }
}
