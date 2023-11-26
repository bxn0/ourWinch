using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;
using ourWinch.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ourWinch.Controllers.Checklist
{
    /// <summary>
    /// Controller for managing mechanical-related operations within the application.
    /// Access to the controller's actions requires authorization.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class MechanicalController : Controller
    {
        /// <summary>
        /// The database context used for data operations.
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// The service for managing service forms related to mechanical components.
        /// </summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;
        /// <summary>
        /// The notification service for sending user notifications.
        /// </summary>
        private readonly INotyfService _irisService;


        /// <summary>
        /// Initializes a new instance of the <see cref="MechanicalController" /> with the specified services.
        /// </summary>
        /// <param name="context">The database context to be used for data operations.</param>
        /// <param name="serviceSkjemaService">The service for managing service forms.</param>
        /// <param name="irisService">The notification service for user communication.</param>

        // Dependency Injection is used to add AppDbContext and ServiceSkjemaService
        public MechanicalController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, INotyfService irisService)
        {
            _context = context;
            _serviceSkjemaService = serviceSkjemaService;
            _irisService = irisService;
        }

        /// <summary>
        /// Asynchronously retrieves all Mechanical entities from the database and provides them to the Index view.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the IActionResult for the Index view, which displays a list of Mechanical entities.
        /// </returns>
        // GET: Mechanical
        public async Task<IActionResult> Index()
        {
            // Retrieves all Mechanical entities asynchronously and passes them to the Index view.
            return View(await _context.Mechanicals.ToListAsync());
        }

        /// <summary>
        /// Retrieves the details of a service order and displays them in the corresponding category view.
        /// </summary>
        /// <param name="id">The ID of the service order to retrieve.</param>
        /// <param name="category">The category of details to display, with a default of "Mechanical".</param>
        /// <returns>
        /// The view corresponding to the specified category with the service order details if found, otherwise NotFound.
        /// </returns>
        // GET: Details
        public IActionResult Details(int id, string category = "Mechanical")
        {
            // Retrieve the service order by ID.
            var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == id);

            // If no service order is found, return NotFound.
            if (serviceOrder == null)
            {
                return NotFound();
            }

            // Determine the view model and view based on the category.
            switch (category)
            {
                case "Mechanical":
                    var mechanicalModel = new MechanicalListViewModel
                    {
                        // Populate the view model with data from the service order.
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
                    // Return the Mechanical create view, pre-populated with the service order details.
                    return View("~/Views/Mechanical/create.cshtml", mechanicalModel);

                default:
                    // If the category is not recognized, return NotFound.
                    return NotFound();
            }
        }

        /// <summary>
        /// Prepares and displays the Create view for a new Mechanical entity, optionally pre-populated with data from an associated service order.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to pre-populate the new Mechanical entity form, if available.</param>
        /// <param name="category">The category of the service order, with a default value of "Mechanical".</param>
        /// <returns>
        /// If the service order is found, the Create view for a Mechanical entity is displayed, pre-populated with data from the service order.
        /// If the service order is not found, NotFound (HTTP 404) is returned.
        /// </returns>
        /// <remarks>
        /// If there is a success message stored in TempData, it will be displayed using the notification service.
        /// </remarks>
        // GET: Mechanical/Create

        [Route("Mechanical/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "Mechanical")
        {
            // Display a success message from TempData if it exists.
            if (TempData["SuccessMessageTrykk"] != null)
            {
                _irisService.Success("SuccessMessageHydroulic", 3); // Typo in the message key? Should be "SuccessMessageTrykk" to match TempData.
            }

            // Retrieve the service order from the database.
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);

            // If the service order with the given ID doesn't exist, return NotFound.
            if (serviceOrder == null)
            {
                return NotFound();
            }

            // Create a new view model pre-populated with data from the service order.
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

            // Set the active category in the view for UI purposes.
            ViewBag.ActiveButton = category;

            // Return the Create view with the view model.
            return View(viewModel);
        }


        /// <summary>
        /// Processes the submission of the Create form for new Mechanical entities.
        /// </summary>
        /// <param name="viewModel">The view model containing the data for new Mechanical entities.</param>
        /// <param name="serviceOrderId">The ID of the service order associated with the new Mechanical entities.</param>
        /// <returns>
        /// If the model state is valid and the new Mechanical entities are created successfully, redirects to the Create action of the Hydrolisk controller.
        /// If no last service order is found or if the model state is not valid, the view is re-displayed with the current viewModel and any error messages.
        /// </returns>
        /// <remarks>
        /// The method sets the first Mechanical entity's comment from the view model and assigns the order number and service order ID to each Mechanical entity.
        /// A success notification is displayed if the entities are saved successfully.
        /// </remarks>
        // POST: Mechanical/Create
        [HttpPost]
        [Route("Mechanical/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MechanicalListViewModel viewModel, int serviceOrderId)
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
                    foreach (var mechanical in viewModel.Mechanicals)
                    {
                        // If this is the first iteration, copy the comment from the view model.
                        if (isFirst)
                        {
                            mechanical.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Set the order number and service order ID for each Mechanical entity.
                        mechanical.Ordrenummer = lastServiceOrder.Ordrenummer;
                        mechanical.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        // Add the new Mechanical entity to the context.
                        _context.Add(mechanical);
                    }

                    // Save all changes to the database asynchronously.
                    await _context.SaveChangesAsync();
                    // Display a success notification.
                    _irisService.Success("Listen ble lagret med suksess.", 3);

                    // Call the service method to update the service schema if all components are completed.
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);

                    // Redirect to the Create action of the Hydrolisk controller with the appropriate parameters.
                    return RedirectToAction("Create", "Hydrolisk", new { serviceOrderId = viewModel.ServiceOrderId, category = "Hydrolisk" });
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
        /// Asynchronously retrieves a Mechanical entity for editing.
        /// </summary>
        /// <param name="id">The ID of the Mechanical entity to be edited. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Edit view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Edit view for the Mechanical entity is displayed.
        /// </returns>
        // GET: Mechanical/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Validate the ID is not null.

            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the Mechanical entity by the provided ID asynchronously.
            var mechanical = await _context.Mechanicals.FindAsync(id);

            // If no entity with the given ID is found, return NotFound.
            if (mechanical == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Edit view with the entity data.
            return View(mechanical);
        }

        /// <summary>
        /// Asynchronously processes the submitted edit form for a Mechanical entity.
        /// </summary>
        /// <param name="id">The ID of the Mechanical entity being edited.</param>
        /// <param name="mechanical">The Mechanical entity that has been bound from the edit form. The properties bound are 'Id', 'ChecklistItem', 'OK', 'BorSkiftes', and 'Defekt'.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult.
        /// If the id does not match the entity's ID, NotFound (HTTP 404) is returned.
        /// If the model state is valid, the changes are saved and the method redirects to the Index action.
        /// If there is a concurrency error and the Mechanical entity no longer exists, NotFound is returned.
        /// Otherwise, the Edit view is re-displayed with the current Mechanical entity.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency error is encountered while saving the Mechanical entity.</exception>
        // POST: Mechanical/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] Mechanical mechanical)
        {
            // Check if the ID in the URL matches the entity's ID.
            if (id != mechanical.Id)
            {
                return NotFound();
            }

            // If the model state is valid, attempt to update and save the entity.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the Mechanical entity still exists.
                    if (!MechanicalExists(mechanical.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw the concurrency exception if the Mechanical entity exists.
                        throw;
                    }
                }
                // If successful, redirect to the Index action.
                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, re-display the Edit view with the current Mechanical entity.
            return View(mechanical);
        }

        /// <summary>
        /// Asynchronously retrieves a Mechanical entity to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the Mechanical entity to delete. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Delete confirmation view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Delete confirmation view for the Mechanical entity is displayed.
        /// </returns>
        // GET: Mechanical/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate that the ID is not null.
            if (id == null)
            {
                return NotFound();
            }
            // Attempt to find the Mechanical entity by the provided ID asynchronously.
            var mechanical = await _context.Mechanicals
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (mechanical == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Delete confirmation view with the entity data.
            return View(mechanical);
        }

        /// <summary>
        /// Asynchronously deletes the specified Mechanical entity from the database.
        /// </summary>
        /// <param name="id">The ID of the Mechanical entity to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult that redirects to the Index action upon successful deletion.
        /// </returns>
        /// <remarks>
        /// This POST action is confirmed via the ActionName attribute and is protected against Cross-Site Request Forgery (CSRF) attacks using the ValidateAntiForgeryToken attribute.
        /// </remarks>
        // POST: Mechanical/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the Mechanical entity by ID asynchronously.
            var mechanical = await _context.Mechanicals.FindAsync(id);
            // Remove the found entity from the database context.
            _context.Mechanicals.Remove(mechanical);
            // Save the changes to the database asynchronously.
            await _context.SaveChangesAsync();
            // Redirect to the Index action of the controller.
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a Mechanical entity with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Mechanical entity to check for existence.</param>
        /// <returns>
        /// True if the entity exists; otherwise, false.
        /// </returns>
        private bool MechanicalExists(int id)
        {
            // Use LINQ to check for the existence of the Mechanical entity with the given ID.
            return _context.Mechanicals.Any(e => e.Id == id);
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
