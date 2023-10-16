using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    public class HydroliskController : Controller
    {
        private readonly AppDbContext _context;

        public HydroliskController(AppDbContext context)
        {
            _context = context;
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

        // GET: Hydrolisk/Create
        public IActionResult Create()
        {
            var viewModel = new HydroliskListViewModel
            {
                Hydrolisks = new List<Hydrolisk>() // İsterseniz bu listeyi doldurabilirsiniz.
            };
            return View(viewModel);
        }

        // POST: Hydrolisk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HydroliskListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // En son eklenen ServiceOrder'ı alıyoruz.
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

                        // Her bir Hydrolisk için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        hydrolisk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        hydrolisk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(hydrolisk);
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
    }
}
