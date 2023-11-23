using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ourWinch.Services;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ourWinch.Controllers.Checklist
{
    /// <summary>
    /// The ElectroController handles all the electro-related operations.
    /// It requires authorization to access its methods.
    /// </summary>
    [Authorize]
    public class ElectroController : Controller
    {
        /// <summary>
        /// The database context used for data operations related to electro.
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// Service for managing service forms specific to electro operations.
        /// </summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;
        /// <summary>
        /// Notification service to communicate with users or systems, named Iris for project-specific reasons.
        /// </summary>
        private readonly INotyfService _irisService;

        // Constructors, methods, and other members would follow with similar documentation.

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectroController"/> class.
        /// </summary>
        /// <param name="context">The database context to be used for data operations.</param>
        /// <param name="serviceSkjemaService">The service for handling service schematics.</param>
        /// <param name="irisService">The notification service for user communication.</param>
        public ElectroController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _irisService = irisService;
        }

        /// <summary>
        /// Asynchronously gets the list of Mechanicals from the database context and returns the Index view.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult of the Index view.</returns>

        public async Task<IActionResult> Index()
        {
            // Retrieves all entries for Mechanicals from the database and converts them to a list asynchronously.
            return View(await _context.Mechanicals.ToListAsync());
        }

        /// <summary>
        /// Asynchronously retrieves and displays the details of a specific electro item.
        /// </summary>
        /// <param name="id">The ID of the electro item to find. Nullable.</param>
        /// <returns>
        /// If the id is null or an electro item with the specified id is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Details view for the electro item is returned.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id parameter is null.
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the electro item by the provided ID asynchronously.
            var electro = await _context.Electros.FirstOrDefaultAsync(m => m.Id == id);

            // If an electro item with the provided ID was not found, return NotFound (HTTP 404).
            if (electro == null)
            {
                return NotFound();
            }

            // If an electro item is found, pass it to the Details view and return the view result.

            return View(electro);
        }

        /// <summary>
        /// Initiates the creation of a new Electro record, pre-populating the form with data from an associated service order.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to associate with the new Electro record.</param>
        /// <param name="category">The category of the item, with a default value of "Electro".</param>
        /// <returns>
        /// If the service order is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, returns the Create view pre-populated with data from the service order.
        /// </returns>
        /// <remarks>
        /// This method also checks for a success message from previous operations stored in TempData and 
        /// uses the notification service to display it.
        /// </remarks>
        [HttpGet]
        [Route("Electro/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Electro")
        {
            // Check for a success message from TempData and display it using the notification service.


            if (TempData["SuccessMessageHydroulic"] != null)
            {
                _irisService.Success("SuccessMessageHydroulic", 3);
            }

            // Find the service order by ID.
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);

            // If the service order with the given ID doesn't exist, return NotFound (HTTP 404).

            if (serviceOrder == null)
            {
                return NotFound();
            }

            // Create and populate the view model with data from the service order.

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

            // Set the active button in the view.
            ViewBag.ActiveButton = category;

            // Return the Create view with the view model.
            return View(viewModel);
        }

        /// <summary>
        /// Handles the post request to create new Electro records based on the provided view model.
        /// </summary>
        /// <param name="viewModel">The view model containing the data for the new Electro records.</param>
        /// <param name="serviceOrderId">The ID of the service order to associate with the new Electro records.</param>
        /// <returns>
        /// If the model state is valid and the operation is successful, redirects to the Create action of the FunksjonsTest controller.
        /// If the last service order cannot be found, adds a model error.
        /// If the model state is not valid, displays the same view with errors.
        /// </returns>
        /// <remarks>
        /// This method performs additional operations such as updating the service schema if all components are completed,
        /// and displays a success message upon saving the list.
        /// </remarks>
       
        [HttpPost]
        [Route("Electro/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ElectroListViewModel viewModel, int serviceOrderId)
        {
            // Check if the provided data in the view model is valid.
            if (ModelState.IsValid)
            {
                bool isFirst = true;
                // Retrieve the last service order by descending order.

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

                        electro.Ordrenummer = lastServiceOrder.Ordrenummer;
                        electro.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        _context.Add(electro);
                    }
                    await _context.SaveChangesAsync();
                    _irisService.Success("Listen ble lagret med suksess.",3);
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);

                    return RedirectToAction("Create", "FunksjonsTest", new { serviceOrderId = viewModel.ServiceOrderId, category = "FunksjonsTest" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ServiceOrder bulunamadı.");
                }
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            return View(viewModel);
        }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electro = await _context.Electros.FirstOrDefaultAsync(m => m.Id == id);
            if (electro == null)
            {
                return NotFound();
            }

            return View(electro);
        }

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

        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
