using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{


    /// <summary>
    /// Controller responsible for handling pressure ("Trykk") related operations within the application.
    /// Access to the controller's actions requires authorization.
    /// </summary>
    [Authorize]
    public class TrykkController : Controller
    {
        /// <summary>The database context used for data operations.</summary>
        private readonly AppDbContext _context;

        /// <summary>A service for managing service schematics.</summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;

        /// <summary>A logger for logging information, warnings, and errors.</summary>
        private readonly ILogger<TrykkController> _logger;

        /// <summary>A notification service for user communication.</summary>
        private readonly INotyfService _irisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrykkController"/> class.
        /// </summary>
        /// <param name="context">The application's database context for data operations.</param>
        /// <param name="serviceSkjemaService">The service for managing service schematics.</param>
        /// <param name="logger">The logger for capturing log information.</param>
        /// <param name="irisService">The notification service for user communication.</param>
        /// <remarks>
        /// Dependency injection is used to provide the controller with the necessary services and context for operation.
        /// </remarks>
        public TrykkController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, ILogger<TrykkController> logger, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _logger = logger;
            _irisService = irisService;
        }


        // GET: Trykk
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mechanicals.ToListAsync());
        }


        // GET: Trykk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trykk = await _context.Trykks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trykk == null)
            {
                return NotFound();
            }

            return View(trykk);
        }

        [HttpGet]
        [Route("Trykk/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Trykk")
        {
            if (TempData["SuccessMessageFunksjons"] != null)
            {
                _irisService.Success("SuccessMessageFunksjons", 3);
            }
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            var viewModel = new TrykkListViewModel
            {
                ServiceOrderId = serviceOrder.ServiceOrderId,
                Ordrenummer = serviceOrder.Ordrenummer,
                Produkttype = serviceOrder.Produkttype,
                Årsmodell = serviceOrder.Årsmodell,
                Fornavn = serviceOrder.Fornavn,
                Etternavn = serviceOrder.Etternavn,
                Serienummer = serviceOrder.Serienummer,
                Status = serviceOrder.Status,
                MobilNo = serviceOrder.MobilNo,
                Feilbeskrivelse = serviceOrder.Feilbeskrivelse,
                KommentarFraKunde = serviceOrder.KommentarFraKunde
            };

            ViewBag.ActiveButton = category;
            return View(viewModel);
        }

        // POST: Trykk/Create
        [HttpPost]
        [Route("Trykk/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrykkListViewModel viewModel, int serviceOrderId)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // En son eklenen ServiceOrder'ı alıyoruz.
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var trykk in viewModel.Trykks)
                    {
                        if (isFirst)
                        {
                            trykk.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Her bir trykk için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        trykk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        trykk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(trykk);
                    }
                    await _context.SaveChangesAsync();
                    _irisService.Success("Listen ble lagret med suksess.", 3);
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);
                    return RedirectToAction("Create", "Mechanical", new { serviceOrderId = viewModel.ServiceOrderId, category = "Mechanical" });
                }
                else
                {
                    // Eğer hiç ServiceOrder bulunamazsa bir hata mesajı döndürebilirsiniz.
                    ModelState.AddModelError(string.Empty, "ServiceOrder not found.");
                }
            }
            // ModelState.IsValid değilse hataları yazdırıyoruz.
            else
            {
                foreach (var modelState in ModelState)
                {
                    var fieldName = modelState.Key;
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Alan: {fieldName}, Hata Mesajı: {error.ErrorMessage}");
                    }
                }
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            return View(viewModel);
        }


        // GET: Trykk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trykk = await _context.Trykks.FindAsync(id);
            if (trykk == null)
            {
                return NotFound();
            }
            return View(trykk);
        }

        // POST: Trykk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Trykk trykk)
        {
            if (id != trykk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trykk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectroExists(trykk.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trykk);
        }

        // GET: Trykk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trykk = await _context.Trykks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trykk == null)
            {
                return NotFound();
            }

            return View(trykk);
        }

        // POST: Trykk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trykk = await _context.Trykks.FindAsync(id);
            if (trykk != null)
            {
                _context.Trykks.Remove(trykk);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case when 'trykk' is null, for example, log an error or return a not found response.
                _logger.LogWarning("Tried to delete Trykk with ID {ID}, but it does not exist.", id);
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool ElectroExists(int id)
        {
            return _context.Trykks.Any(e => e.Id == id);
        }
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
