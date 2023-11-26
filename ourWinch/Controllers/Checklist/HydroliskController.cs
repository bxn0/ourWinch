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
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class HydroliskController : Controller
    {
        /// <summary>
        /// The controller that handles hydraulic-related operations.
        /// Authorization is required to access its methods.
        /// </summary>
        private readonly AppDbContext _context;

        // Newly added service
        /// <summary>
        /// The service skjema service
        /// </summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;
        /// <summary>
        /// The iris service
        /// </summary>
        private readonly INotyfService _irisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydroliskController" /> class with necessary dependencies.
        /// </summary>
        /// <param name="context">The database context used for data access operations.</param>
        /// <param name="serviceSkjemaService">A service for managing service schematics within the application.</param>
        /// <param name="irisService">A service for sending notifications to the user interface.</param>
        public HydroliskController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _irisService = irisService;
        }

        /// <summary>
        /// Asynchronously retrieves all Mechanical entities related to Hydrolisk from the database and displays them on the index view.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Index view,
        /// displaying a list of Mechanical entities.
        /// </returns>
        // GET: Hydrolisk
        public async Task<IActionResult> Index()
        {
            // Retrieves all Mechanical entities asynchronously and passes them to the Index view.
            return View(await _context.Mechanicals.ToListAsync());
        }

        /// <summary>
        /// Asynchronously retrieves and displays the details of a specific Hydrolisk entity.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity to find. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Details view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Details view for the Hydrolisk entity is displayed.
        /// </returns>
        // GET: Mechanical/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Validate the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Hydrolisk entity by the provided ID asynchronously.
            var hydrolisk = await _context.Hydrolisks
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (hydrolisk == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Details view with the entity data.
            return View(hydrolisk);
        }

        /// <summary>
        /// Prepares and displays the Create view for a new Hydrolisk entity, pre-populated with data from an associated service order.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to pre-populate the new Hydrolisk entity form.</param>
        /// <param name="category">The category of the service order, with a default value of "Hydrolisk".</param>
        /// <returns>
        /// If the service order is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Create view for a Hydrolisk entity is displayed, pre-populated with data from the service order.
        /// </returns>
        /// <remarks>
        /// If there is a success message stored in TempData, it will be displayed using the notification service.
        /// </remarks>
        [HttpGet]
        [Route("Hydrolisk/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Hydrolisk")
        {
            // Display success message from TempData if it exists.
            if (TempData["SuccessMessageMechanical"] != null)
            {
                _irisService.Success("SuccessMessageMechanical", 3);
            }

            // Retrieve the service order from the database.
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);

            // If the service order with the given ID doesn't exist, return NotFound.
            if (serviceOrder == null)
            {
                return NotFound();
            }

            // Create a new view model pre-populated with data from the service order.
            var viewModel = new HydroliskListViewModel
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
        /// Processes the submission of the Create form for new Hydrolisk entities.
        /// </summary>
        /// <param name="viewModel">The view model containing the data for new Hydrolisk entities.</param>
        /// <param name="serviceOrderId">The ID of the service order associated with the new Hydrolisk entities.</param>
        /// <returns>
        /// If the model state is valid and the new Hydrolisk entities are created successfully, redirects to the Create action of the Electro controller.
        /// If no last service order is found or if the model state is not valid, the view is re-displayed with the current viewModel and any error messages.
        /// </returns>
        /// <remarks>
        /// The method sets the first Hydrolisk entity's comment from the view model and assigns the order number and service order ID to each Hydrolisk entity.
        /// A success message is displayed if the entities are saved successfully.
        /// The UpdateServicejemaIfAllCompleted service method is called to update the service schema.
        /// </remarks>
        // POST: Hydrolisk/Create
        [HttpPost]
        [Route("Hydrolisk/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HydroliskListViewModel viewModel, int serviceOrderId)
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
                    foreach (var hydrolisk in viewModel.Hydrolisks)
                    {
                        // If this is the first iteration, copy the comment from the view model.

                        if (isFirst)
                        {
                            hydrolisk.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Set the order number and service order ID for each Hydrolisk entity.
                        hydrolisk.Ordrenummer = lastServiceOrder.Ordrenummer;
                        hydrolisk.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        // Add the new Hydrolisk entity to the context.
                        _context.Add(hydrolisk);
                    }
                    // Save all changes to the database asynchronously.
                    await _context.SaveChangesAsync();
                    // Display a success message.
                    _irisService.Success("Listen ble lagret med suksess.", 3);

                    // Call the newly added service method to update the service schema.
                    await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId); 

                    // Redirect to the Create action of the Electro controller with the appropriate parameters.
                    return RedirectToAction("Create", "Electro", new { serviceOrderId = viewModel.ServiceOrderId, category = "Electro" });
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
                // If the model state is not valid, collect the error messages to display.
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            // Return the view with the provided view model, including any errors.
            return View(viewModel);
        }

        /// <summary>
        /// Asynchronously retrieves a Hydrolisk entity for editing.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity to be edited. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Edit view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Edit view for the Hydrolisk entity is displayed.
        /// </returns>
        // GET: Hydrolisk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Validate the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Hydrolisk entity by the provided ID asynchronously.
            var hydrolisk = await _context.Hydrolisks.FindAsync(id);

            // If no entity with the given ID is found, return NotFound.
            if (hydrolisk == null)
            {
                return NotFound();
            }
            // If an entity is found, return the Edit view with the entity data.
            return View(hydrolisk);
        }

        /// <summary>
        /// Asynchronously processes the submitted edit form for a Hydrolisk entity.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity being edited.</param>
        /// <param name="hydrolisk">The Hydrolisk entity that has been bound from the edit form. The properties bound are 'Id', 'ChecklistItem', 'OK', 'BorSkiftes', and 'Defekt'.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult.
        /// If the id does not match the entity's ID, NotFound (HTTP 404) is returned.
        /// If the model state is valid, the changes are saved and the method redirects to the Index action.
        /// If there is a concurrency error and the Hydrolisk entity no longer exists, NotFound is returned.
        /// Otherwise, the Edit view is re-displayed with the current Hydrolisk entity.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency error is encountered while saving the Hydrolisk entity.</exception>
        // POST: Hydrolisk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Hydrolisk hydrolisk)
        {
            // Check if the ID in the URL matches the entity's ID.
            if (id != hydrolisk.Id)
            {
                return NotFound();
            }

            // If the model state is valid, attempt to update and save the entity.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hydrolisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the Hydrolisk entity still exists.
                    if (!HydroliskExists(hydrolisk.Id)) // Should be HydroliskExists, not ElectroExists.
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw the concurrency exception if the Hydrolisk entity exists.

                        throw;
                    }
                }

                // If successful, redirect to the Index action.
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, re-display the Edit view with the current Hydrolisk entity.
            return View(hydrolisk);
        }

        /// <summary>
        /// Asynchronously retrieves a Hydrolisk entity to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity to delete. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Delete confirmation view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Delete confirmation view for the Hydrolisk entity is displayed.
        /// </returns>
        // GET: Hydrolisk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Hydrolisk entity by the provided ID asynchronously.
            var hydrolisk = await _context.Hydrolisks
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (hydrolisk == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Delete confirmation view with the entity data.
            return View(hydrolisk);
        }

        /// <summary>
        /// Asynchronously deletes the specified Hydrolisk entity from the database.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult that redirects to the Index action upon successful deletion.
        /// </returns>
        /// <remarks>
        /// This POST action is confirmed via the ActionName attribute and is protected against Cross-Site Request Forgery (CSRF) attacks using the ValidateAntiForgeryToken attribute.
        /// </remarks>
        // POST: Hydrolisk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the Hydrolisk entity by ID asynchronously.
            var hydrolisk = await _context.Hydrolisks.FindAsync(id);
            // Remove the found entity from the database context.
            _context.Hydrolisks.Remove(hydrolisk);
            // Save the changes to the database asynchronously.
            await _context.SaveChangesAsync();
            // Redirect to the Index action of the controller.
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a Hydrolisk entity with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Hydrolisk entity to check for existence.</param>
        /// <returns>
        /// True if the entity exists; otherwise, false.
        /// </returns>
        private bool HydroliskExists(int id) // The method name should reflect the entity it checks for, e.g., HydroliskExists.

        {
            // Use LINQ to check for the existence of the Hydrolisk entity with the given ID.
            return _context.Hydrolisks.Any(e => e.Id == id);
        }
        /// <summary>
        /// Asynchronously updates the service schema to reflect that all associated tasks are completed if applicable.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to check and update.</param>
        /// <returns>
        /// A task that represents the asynchronous update operation.
        /// </returns>
        private async Task UpdateServicejemaIfAllCompleted(int serviceOrderId)
        {
            // Await the completion of the update operation on the service schema.
            await _serviceSkjemaService.UpdateServicejemaIfAllCompleted(serviceOrderId);

        }
    }
}
