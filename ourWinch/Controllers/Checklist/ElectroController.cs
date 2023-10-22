using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    public class ElectroController : Controller
    {
        private readonly AppDbContext _context;

        public ElectroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Electro
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

            var electro = await _context.Electros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electro == null)
            {
                return NotFound();
            }

            return View(electro);
        }

        // GET: Electro/Create
        [Route("Electro/Create/{serviceOrderId}")]
        public IActionResult Create(int serviceOrderId)
        {
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            var viewModel = new ElectroListViewModel
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

        // POST: Electro/Create
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
                    foreach (var electro in viewModel.Electros)
                    {
                        if (isFirst)
                        {
                            electro.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Her bir Electro için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        electro.Ordrenummer = lastServiceOrder.Ordrenummer;
                        electro.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(electro);
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


        // GET: Electro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electro = await _context.Electros.FindAsync(id);
            if (electro == null)
            {
                return NotFound();
            }
            return View(electro);
        }

        // POST: Electro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Electro electro)
        {
            if (id != electro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectroExists(electro.Id))
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
            return View(electro);
        }

        // GET: Electro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electro = await _context.Electros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electro == null)
            {
                return NotFound();
            }

            return View(electro);
        }

        // POST: Electro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electro = await _context.Electros.FindAsync(id);
            _context.Electros.Remove(electro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectroExists(int id)
        {
            return _context.Electros.Any(e => e.Id == id);
        }
    }
}
