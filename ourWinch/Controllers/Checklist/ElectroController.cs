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
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
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
        /// Initializes a new instance of the <see cref="ElectroController" /> class.
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
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult of the Index view.
        /// </returns>

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


            // var errors = ModelState.Values.SelectMany(x => x.Errors);
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
                        // If this is the first iteration, copy the comment from the view model.
                        if (isFirst)
                        {
                            electro.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Set the order number and service order ID for each electro item.

                        electro.Ordrenummer = lastServiceOrder.Ordrenummer;
                        electro.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        // Add the new electro item to the context.
                        _context.Add(electro);
                    }
                    // Save all changes to the database.
                    await _context.SaveChangesAsync();

                    // Display a success message.
                    _irisService.Success("Listen ble lagret med suksess.",3);

                    // Update the service schema status.
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);

                    // Redirect to the Create action of the FunksjonsTest controller with the appropriate parameters.
                    return RedirectToAction("Create", "FunksjonsTest", new { serviceOrderId = viewModel.ServiceOrderId, category = "FunksjonsTest" });
                }
                else
                {
                    // If no last service order is found, add a model error.
                    ModelState.AddModelError(string.Empty, "ServiceOrder not found.");
                }
            }
            else
            {
                // If the model state is invalid, collect the error messages to display.

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            // Return the view with the provided view model, including any errors.
            return View(viewModel);
        }

        /// <summary>
        /// Asynchronously gets an Electro entity for editing.
        /// </summary>
        /// <param name="id">The ID of the Electro entity to be edited. Nullable.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Edit view.
        /// If the id is null or an Electro entity with the specified id is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Edit view for the Electro entity is displayed.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            // If no ID is provided, return a NotFound result.
            if (id == null)
            {
                return NotFound();
            }

            // Asynchronously find the Electro entity by the given ID.
            var electro = await _context.Electros.FindAsync(id);

            // If no Electro entity is found, return NotFound.
            if (electro == null)
            {
                return NotFound();
            }
            // If an Electro entity is found, pass it to the Edit view and return the view result.
            return View(electro);
        }

        /// <summary>
        /// Asynchronously processes the editing of an existing Electro entity.
        /// </summary>
        /// <param name="id">The ID of the Electro entity being edited.</param>
        /// <param name="electro">The Electro entity that has been bound from the edit form. Properties bound are 'Id', 'ChecklistItem', 'OK', 'BorSkiftes', and 'Defekt'.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult.
        /// If the id does not match the entity's ID, NotFound (HTTP 404) is returned.
        /// If the model state is valid, the changes are saved and redirects to the Index action.
        /// If there is a concurrency error and the Electro entity no longer exists, NotFound is returned.
        /// Otherwise, the Edit view is re-displayed with the current Electro entity.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency error is encountered while saving the Electro entity.</exception>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Electro electro)
        {
            // Check if the ID in the URL matches the entity's ID.
            if (id != electro.Id)
            {
                return NotFound();
            }

            // If the model state is valid, attempt to update and save the entity.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the Electro entity still exists.
                    if (!ElectroExists(electro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw the concurrency exception if the Electro entity exists.

                        throw;
                    }
                }
                // If successful, redirect to the Index action.
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, re-display the Edit view with the current Electro entity.
            return View(electro);
        }

        /// <summary>
        /// Asynchronously retrieves an Electro entity to confirm deletion.
        /// </summary>
        /// <param name="id">The ID of the Electro entity to delete. Nullable.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Delete confirmation view.
        /// If the id is null or an Electro entity with the specified id is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Delete confirmation view for the Electro entity is displayed.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the Electro entity by the provided ID asynchronously.
            var electro = await _context.Electros.FirstOrDefaultAsync(m => m.Id == id);

            // If the Electro entity is not found, return NotFound.
            if (electro == null)
            {
                return NotFound();
            }

            // If an Electro entity is found, pass it to the Delete confirmation view and return the view result.
            return View(electro);
        }

        /// <summary>
        /// Asynchronously deletes the Electro entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Electro entity to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous delete operation and navigation to the Index action upon completion.
        /// </returns>
        /// <remarks>
        /// This is a POST action protected against Cross-Site Request Forgery (CSRF).
        /// The entity is first retrieved and then removed from the database context followed by saving the changes.
        /// </remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electro = await _context.Electros.FindAsync(id);
            _context.Electros.Remove(electro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if an Electro entity with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Electro entity to check.</param>
        /// <returns>
        /// True if the Electro entity exists; otherwise, false.
        /// </returns>
        private bool ElectroExists(int id)
        {
            return _context.Electros.Any(e => e.Id == id);
        }

        /// <summary>
        /// Asynchronously updates the service schema to reflect completion if all associated tasks are completed.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to check for completion.</param>
        /// <returns>
        /// A task that represents the asynchronous update operation.
        /// </returns>
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
