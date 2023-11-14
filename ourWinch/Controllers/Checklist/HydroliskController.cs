using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{


    [Authorize]
    public class HydroliskController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ServiceSkjemaService _serviceSkjemaService; // Eklediğimiz yeni servis

        // Düzenlediğimiz constructor
        public HydroliskController(AppDbContext context, ServiceSkjemaService serviceSkjemaService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
        }

        // GET: Hydrolisk
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mechanicals.ToListAsync());
        }


        // GET: Mechanical/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hydrolisk = await _context.Hydrolisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hydrolisk == null)
            {
                return NotFound();
            }

            return View(hydrolisk);
        }

        [HttpGet]
        [Route("Hydrolisk/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Hydrolisk")
        {
            if (TempData["SuccessMessageMechanical"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessageMechanical"].ToString();
            }
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            var viewModel = new HydroliskListViewModel
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

        // POST: Hydrolisk/Create
        [HttpPost]
        [Route("Hydrolisk/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HydroliskListViewModel viewModel, int serviceOrderId, string category)
        {
          
            if (ModelState.IsValid)
            {
                bool isFirst = true;
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var hydrolisk in viewModel.Hydrolisks)
                    {
                        if (isFirst)
                        {
                            hydrolisk.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        hydrolisk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        hydrolisk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(hydrolisk);
                    }
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessageHydroulic"] = "Hydrauliskesjekklisten ble lagret med suksess.";
                    await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId); // Eklediğimiz yeni servis metodunu çağırıyoruz
                    return RedirectToAction("Create", "Electro", new { serviceOrderId = viewModel.ServiceOrderId, category = "Electro" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ServiceOrder bulunamadı.");
                }
            }
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


        // GET: Hydrolisk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hydrolisk = await _context.Hydrolisks.FindAsync(id);
            if (hydrolisk == null)
            {
                return NotFound();
            }
            return View(hydrolisk);
        }

        // POST: Hydrolisk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Hydrolisk hydrolisk)
        {
            if (id != hydrolisk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hydrolisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectroExists(hydrolisk.Id))
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
            return View(hydrolisk);
        }

        // GET: Hydrolisk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hydrolisk = await _context.Hydrolisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hydrolisk == null)
            {
                return NotFound();
            }

            return View(hydrolisk);
        }

        // POST: Hydrolisk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hydrolisk = await _context.Hydrolisks.FindAsync(id);
            _context.Hydrolisks.Remove(hydrolisk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectroExists(int id)
        {
            return _context.Hydrolisks.Any(e => e.Id == id);
        }
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
