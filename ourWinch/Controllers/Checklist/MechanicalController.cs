using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;
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

        [Route("Mechanical/Create/{serviceOrderId}")]
        public IActionResult Create(int serviceOrderId)
        {
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

            return View(viewModel);
        }




        // POST: Mechanical/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MechanicalListViewModel viewModel)
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

                        // Her bir Mechanical için ServiceOrder'dan Ordrenummer'ı alıyoruz.
                        mechanical.Ordrenummer = lastServiceOrder.Ordrenummer;
                        mechanical.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(mechanical);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "Mechanical");
                }
                else
                {
                    // Eğer hiç ServiceOrder bulunamazsa bir hata mesajı döndürebilirsiniz.
                    ModelState.AddModelError(string.Empty, "ServiceOrder bulunamadı.");
                }
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
