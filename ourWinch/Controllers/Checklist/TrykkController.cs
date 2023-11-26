using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{


    /// <summary>
    /// Controller responsible for handling pressure ("Trykk") related operations within the application.
    /// Access to the controller's actions requires authorization.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class TrykkController : Controller
    {
        /// <summary>
        /// The database context used for data operations.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// A service for managing service schematics.
        /// </summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;

        /// <summary>
        /// A logger for logging information, warnings, and errors.
        /// </summary>
        private readonly ILogger<TrykkController> _logger;

        /// <summary>
        /// A notification service for user communication.
        /// </summary>
        private readonly INotyfService _irisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrykkController" /> class.
        /// </summary>
        /// <param name="context">The application's database context for data operations.</param>
        /// <param name="serviceSkjemaService">The service for managing service schematics.</param>
        /// <param name="logger">The logger for capturing log information.</param>
        /// <param name="irisService">The notification service for user communication.</param>
        /// <remarks>
        /// Dependency injection is used to provide the controller with the necessary services and context for operation.
        /// </remarks>
        public TrykkController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, ILogger<TrykkController> logger, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _logger = logger;
            _irisService = irisService;
        }

        /// <summary>
        /// Asynchronously retrieves all Mechanical entities from the database and provides them to the Index view for the Trykk section.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the IActionResult for the Index view, which displays a list of Mechanical entities.
        /// </returns>
        // GET: Trykk
        public async Task<IActionResult> Index()
        {
            // Retrieves all Mechanical entities asynchronously and passes them to the Index view of the Trykk section.
            return View(await _context.Mechanicals.ToListAsync());
        }

        /// <summary>
        /// Asynchronously retrieves and displays the details of a specific Trykk entity.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity to be displayed. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Details view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Details view for the Trykk entity is displayed.
        /// </returns>
        // GET: Trykk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Validate that the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Trykk entity by the provided ID asynchronously.
            var trykk = await _context.Trykks
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (trykk == null)
            {
                return NotFound();
            }
            // If an entity is found, return the Details view with the entity data.
            return View(trykk);
        }

        /// <summary>
        /// Prepares and displays the Create view for a new Trykk entity, pre-populated with data from an associated service order if available.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to pre-populate the new Trykk entity form.</param>
        /// <param name="category">The category of the service order, with a default value of "Trykk".</param>
        /// <returns>
        /// If the service order is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Create view for a Trykk entity is displayed, pre-populated with data from the service order.
        /// </returns>
        /// <remarks>
        /// If there is a success message stored in TempData, it will be displayed using the notification service.
        /// </remarks>
        [HttpGet]
        [Route("Trykk/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Trykk")
        {
            // var errors = ModelState.Values.SelectMany(x => x.Errors);
            // Display success message from TempData if it exists.
            if (TempData["SuccessMessageFunksjons"] != null)
            {
                _irisService.Success("SuccessMessageFunksjons", 3);
            }

            // Retrieve the service order from the database.
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);

            // If the service order with the given ID doesn't exist, return NotFound.
            if (serviceOrder == null)
            {
                return NotFound();
            }
            // Create a new view model pre-populated with data from the service order.
            var viewModel = new TrykkListViewModel
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
            // Set the active category in the view for UI purposes.
            ViewBag.ActiveButton = category;
            // Return the Create view with the view model.
            return View(viewModel);
        }

        /// <summary>
        /// Processes the submission of the Create form for new Trykk (pressure) entities.
        /// </summary>
        /// <param name="viewModel">The view model containing the data for new Trykk entities.</param>
        /// <param name="serviceOrderId">The ID of the service order associated with the new Trykk entities.</param>
        /// <returns>
        /// If the model state is valid and the new Trykk entities are created successfully, redirects to the Create action of the Mechanical controller.
        /// If no last service order is found or if the model state is not valid, the view is re-displayed with the current viewModel and any error messages.
        /// </returns>
        /// <remarks>
        /// The method sets the first Trykk entity's comment from the view model and assigns the order number and service order ID to each Trykk entity.
        /// A success notification is displayed if the entities are saved successfully.
        /// </remarks>
        // POST: Trykk/Create
        [HttpPost]
        [Route("Trykk/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrykkListViewModel viewModel, int serviceOrderId)
        {
            // Check if the provided data in the view model is valid.
            if (ModelState.IsValid)
            {
                bool isFirst = true;

                // Retrieve the last service order by descending order.
                var lastServiceOrder = _context.ServiceOrders.OrderByDescending(o => o.ServiceOrderId).FirstOrDefault();

                if (lastServiceOrder != null)
                {
                    foreach (var trykk in viewModel.Trykks)
                    {
                        // If this is the first iteration, copy the comment from the view model.
                        if (isFirst)
                        {
                            trykk.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Set the order number and service order ID for each Trykk entity.
                        trykk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        trykk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        // Add the new Trykk entity to the context.
                        _context.Add(trykk);
                    }
                    // Save all changes to the database asynchronously.
                    await _context.SaveChangesAsync();
                    // Display a success notification.
                    _irisService.Success("Listen ble lagret med suksess.", 3);

                    // Call the service method to update the service schema if all components are completed.
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);

                    // Redirect to the Create action of the Mechanical controller with the appropriate parameters.
                    return RedirectToAction("Create", "Mechanical", new { serviceOrderId = viewModel.ServiceOrderId, category = "Mechanical" });
                }
                else
                {
                    // If no last service order is found, add a model error.
                    ModelState.AddModelError(string.Empty, "ServiceOrder not found.");
                }
            }
           
            else
            {
                foreach (var modelState in ModelState)
                {
                    var fieldName = modelState.Key;
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Field: {fieldName}, Error message: {error.ErrorMessage}");
                    }
                }
                // If the model state is not valid, collect and display the error messages.
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            // Return the view with the provided view model, including any errors.
            return View(viewModel);
        }

        /// <summary>
        /// Asynchronously retrieves a Trykk entity for editing.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity to be edited. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Edit view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Edit view for the Trykk entity is displayed.
        /// </returns>
        // GET: Trykk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Validate that the ID is not null.
            if (id == null)
            {
                return NotFound();
            }
            // Attempt to find the Trykk entity by the provided ID asynchronously.
            var trykk = await _context.Trykks.FindAsync(id);

            // If no entity with the given ID is found, return NotFound.
            if (trykk == null)
            {
                return NotFound();
            }
            // If an entity is found, return the Edit view with the entity data.
            return View(trykk);
        }

        /// <summary>
        /// Asynchronously processes the submitted edit form for a Trykk entity.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity being edited.</param>
        /// <param name="trykk">The Trykk entity that has been bound from the edit form. Properties bound are 'Id', 'ChecklistItem', 'OK', 'BorSkiftes', and 'Defekt'.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult.
        /// If the id does not match the entity's ID, NotFound (HTTP 404) is returned.
        /// If the model state is valid, the changes are saved and the method redirects to the Index action.
        /// If there is a concurrency error and the Trykk entity no longer exists, NotFound is returned.
        /// Otherwise, the Edit view is re-displayed with the current Trykk entity.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency error is encountered while saving the Trykk entity.</exception>
        // POST: Trykk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Trykk trykk)
        {
            // Check if the ID in the URL matches the entity's ID.
            if (id != trykk.Id)
            {
                return NotFound();
            }
            // If the model state is valid, attempt to update and save the entity.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trykk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the Trykk entity still exists.
                    if (!TrykkExists(trykk.Id)) // Corrected method name from ElectroExists to TrykkExists.
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw the concurrency exception if the Trykk entity exists.
                        throw;
                    }
                }
                // If successful, redirect to the Index action.

                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, re-display the Edit view with the current Trykk entity.

            return View(trykk);
        }

        /// <summary>
        /// Asynchronously retrieves a Trykk entity to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity to delete. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Delete confirmation view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Delete confirmation view for the Trykk entity is displayed.
        /// </returns>
        // GET: Trykk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate that the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Trykk entity by the provided ID asynchronously.
            var trykk = await _context.Trykks
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (trykk == null)
            {
                return NotFound();
            }
            // If an entity is found, return the Delete confirmation view with the entity data.
            return View(trykk);
        }

        /// <summary>
        /// Asynchronously deletes the specified Trykk entity from the database.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous delete operation.
        /// The task result contains an IActionResult that redirects to the Index action upon successful deletion.
        /// If the Trykk entity is not found, NotFound (HTTP 404) is returned.
        /// </returns>
        /// <remarks>
        /// This POST action is confirmed via the ActionName attribute and is protected against Cross-Site Request Forgery (CSRF) attacks using the ValidateAntiForgeryToken attribute.
        /// </remarks>
        // POST: Trykk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trykk = await _context.Trykks.FindAsync(id);
            if (trykk != null)
            {
                _context.Trykks.Remove(trykk);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Log a warning and return NotFound if the Trykk entity does not exist.
                _logger.LogWarning("Tried to delete Trykk with ID {ID}, but it does not exist.", id);
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a Trykk entity with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Trykk entity to check for existence.</param>
        /// <returns>
        /// True if the entity exists; otherwise false.
        /// </returns>
        private bool TrykkExists(int id)
        {
            // Use LINQ to check for the existence of the Trykk entity with the given ID.
            return _context.Trykks.Any(e => e.Id == id);
        }
        /// <summary>
        /// Asynchronously updates the service schema to reflect completion status for all associated tasks of a given service order.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order for which the service schema is to be updated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of updating the service schema.
        /// </returns>
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            // Call the service method to update the service schema if all related tasks are completed.
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
