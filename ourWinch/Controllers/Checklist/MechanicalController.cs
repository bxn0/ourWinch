using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;
using ourWinch.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{

    [Authorize]
    public class MechanicalController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ServiceSkjemaService _serviceSkjemaService;
        private readonly INotyfService _irisService;

        // Dependency Injection ile AppDbContext ve ServiceSkjemaService ekleniyor
        public MechanicalController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _irisService = irisService;
        }

        // GET: Mechanical
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mechanicals.ToListAsync());
        }

        // GET: Details
        public IActionResult Details(int id, string category = "Mechanical")
        {
            
            var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            switch (category)
            {
                case "Mechanical":
                    var mechanicalModel = new MechanicalListViewModel
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
                    return View("~/Views/Mechanical/create.cshtml", mechanicalModel);

                default:
                    return NotFound();
            }
        }

        // GET: Mechanical/Create

        [Route("Mechanical/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Mechanical")
        {
            if (TempData["SuccessMessageTrykk"] != null)
            {
                _irisService.Success("SuccessMessageHydroulic", 3);
            }
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            var viewModel = new MechanicalListViewModel
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

        // POST: Mechanical/Create
        [HttpPost]
        [Route("Mechanical/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MechanicalListViewModel viewModel, int serviceOrderId)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // En son eklenen ServiceOrder'ı alıyoruz.
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var mechanical in viewModel.Mechanicals)
                    {
                        if (isFirst)
                        {
                            mechanical.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Her bir mechanical için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        mechanical.Ordrenummer = lastServiceOrder.Ordrenummer;
                        mechanical.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(mechanical);
                    }
                    await _context.SaveChangesAsync();
                    //sweatalert feedback
                    _irisService.Success("Listen ble lagret med suksess.", 3);

                    await UpdateServicejemaIfAllCompleted(serviceOrderId);
                    return RedirectToAction("Create", "Hydrolisk", new { serviceOrderId = viewModel.ServiceOrderId, category = "Hydrolisk" });
                }
                else
                {
                    // Eğer hiç ServiceOrder bulunamazsa bir hata mesajı döndürebilirsiniz.
                    ModelState.AddModelError(string.Empty, "ServiceOrder bulunamadı.");
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



        // GET: Mechanical/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanical = await _context.Mechanicals.FindAsync(id);
            if (mechanical == null)
            {
                return NotFound();
            }
            return View(mechanical);
        }

        // POST: Mechanical/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Mechanical mechanical)
        {
            if (id != mechanical.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanicalExists(mechanical.Id))
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
            return View(mechanical);
        }

        // GET: Mechanical/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanical = await _context.Mechanicals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanical == null)
            {
                return NotFound();
            }

         

            return View(mechanical);
        }

        // POST: Mechanical/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mechanical = await _context.Mechanicals.FindAsync(id);
            _context.Mechanicals.Remove(mechanical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanicalExists(int id)
        {
            return _context.Mechanicals.Any(e => e.Id == id);
        }
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
