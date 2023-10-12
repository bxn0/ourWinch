using ourWinchSist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    public class MechanicalController : Controller
    {
        private readonly AppDbContext _context;

        public MechanicalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mechanical
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

            var mechanical = await _context.Mechanicals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanical == null)
            {
                return NotFound();
            }

            return View(mechanical);
        }

        // GET: Mechanical/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mechanical/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SjekkPunkter,OK,BorSkiftes,Defekt")] Mechanical mechanical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mechanical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mechanical);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,SjekkPunkter,OK,BorSkiftes,Defekt")] Mechanical mechanical)
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
    }
}
