using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    public class TrykkController : Controller
    {
        private readonly AppDbContext _context;

        public TrykkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Trykk
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

            var trykk = await _context.Trykks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trykk == null)
            {
                return NotFound();
            }

            return View(trykk);
        }

        // GET: Trykk/Create
        public IActionResult Create()
        {
            var viewModel = new TrykkListViewModel
            {
                Trykks = new List<Trykk>() // İsterseniz bu listeyi doldurabilirsiniz.
            };
            return View(viewModel);
        }

        // POST: Trykk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrykkListViewModel viewModel)
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

                        // Her bir Trykk için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        trykk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        trykk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(trykk);
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
            _context.Trykks.Remove(trykk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectroExists(int id)
        {
            return _context.Trykks.Any(e => e.Id == id);
        }
    }
}
