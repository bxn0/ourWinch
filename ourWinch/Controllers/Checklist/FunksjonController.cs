using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var FunksjonsTest = await _context.Electros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (FunksjonsTest == null)
            {
                return NotFound();
            }

            return View(FunksjonsTest);
        }

        // GET: FunksjonsTest/Create
        public IActionResult Create()
        {
            var viewModel = new FunksjonsTestListViewModel
            {
                FunksjonsTests = new List<FunksjonsTest>() // İsterseniz bu listeyi doldurabilirsiniz.
            };
            return View(viewModel);
        }

        // POST: FunksjonsTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ElectroListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // En son eklenen ServiceOrder'ı alıyoruz.
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var FunksjonsTest in viewModel.Electros)
                    {
                        if (isFirst)
                        {
                            FunksjonsTest.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Her bir FunksjonsTest için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        FunksjonsTest.Ordrenummer = lastServiceOrder.Ordrenummer;
                        FunksjonsTest.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(FunksjonsTest);
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

            var FunksjonsTest = await _context.Electros.FindAsync(id);
            if (FunksjonsTest == null)
            {
                return NotFound();
            }
            return View(FunksjonsTest);
        }

        // POST: FunksjonsTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] FunksjonsTest FunksjonsTest)
        {
            if (id != FunksjonsTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(FunksjonsTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectroExists(FunksjonsTest.Id))
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
            return View(FunksjonsTest);
        }

        // GET: FunksjonsTest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var FunksjonsTest = await _context.Electros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (FunksjonsTest == null)
            {
                return NotFound();
            }

            return View(FunksjonsTest);
        }

        // POST: FunksjonsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var FunksjonsTest = await _context.Electros.FindAsync(id);
            _context.Electros.Remove(FunksjonsTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectroExists(int id)
        {
            return _context.Electros.Any(e => e.Id == id);
        }
    }
}
