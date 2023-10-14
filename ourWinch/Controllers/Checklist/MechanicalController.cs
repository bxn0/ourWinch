﻿using ourWinchSist.Models;
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

        public IActionResult AddTestMechanical()
        {
            var mechanical = new Mechanical
            {
                OrderID = 123,
                ChecklistItem = "Örnek Madde",
                OK = true,
                BorSkiftes = false,
                Defekt = true,
                Kommentar = "Bu bir yorumdur"
            };

            _context.Mechanicals.Add(mechanical);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create(MechanicalListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var mechanical in viewModel.Mechanicals)
                {
                    _context.Add(mechanical);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
