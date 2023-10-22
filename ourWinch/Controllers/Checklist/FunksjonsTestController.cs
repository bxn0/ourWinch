using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    public class FunksjonsTestController : Controller
    {
        private readonly AppDbContext _context;

        public FunksjonsTestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FunksjonsTest
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

            var funksjonsTest = await _context.FunksjonsTests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funksjonsTest == null)
            {
                return NotFound();
            }

            return View(funksjonsTest);
        }

        // GET: FunksjonsTest/Create
        [Route("FunksjonsTest/Create/{serviceOrderId}")]
        public IActionResult Create(int serviceOrderId)
        {
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            var viewModel = new FunksjonsTestListViewModel
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

            return View(viewModel);
        }

        // POST: FunksjonsTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FunksjonsTestListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // En son eklenen ServiceOrder'ı alıyoruz.
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var funksjonsTest in viewModel.FunksjonsTests)
                    {
                        if (isFirst)
                        {
                            funksjonsTest.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                       
                        funksjonsTest.Ordrenummer = lastServiceOrder.Ordrenummer;
                        funksjonsTest.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(funksjonsTest);
                    }
                    await _context.SaveChangesAsync();
                    return Redirect("/ServiceOrder/Details/1");
                }
                else
                {
                    // Eğer hiç ServiceOrder bulunamazsa bir hata mesajı döndürebilirsiniz.
                    ModelState.AddModelError(string.Empty, "ServiceOrder bulunamadı.");
                }
            }
            return View(viewModel);
        }


        // GET: FunksjonsTest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funksjonsTest = await _context.FunksjonsTests.FindAsync(id);
            if (funksjonsTest == null)
            {
                return NotFound();
            }
            return View(funksjonsTest);
        }

        // POST: FunksjonsTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] FunksjonsTest funksjonsTest)
        {
            if (id != funksjonsTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funksjonsTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunksjonsTestExists(funksjonsTest.Id))
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
            return View(funksjonsTest);
        }

        // GET: FunksjonsTest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funksjonsTest = await _context.FunksjonsTests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funksjonsTest == null)
            {
                return NotFound();
            }

            return View(funksjonsTest);
        }

        // POST: FunksjonsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funksjonsTest = await _context.FunksjonsTests.FindAsync(id);
            _context.FunksjonsTests.Remove(funksjonsTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunksjonsTestExists(int id)
        {
            return _context.FunksjonsTests.Any(e => e.Id == id);
        }
    }
}
