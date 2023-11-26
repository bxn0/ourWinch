using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ourWinch.Services;

namespace ourWinch.Controllers.Checklist
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class FunksjonsTestController : Controller
    {
        /// <summary>
        /// Controller responsible for handling functional testing related operations.
        /// Authorization is required to access its methods.
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// The service skjema service
        /// </summary>
        private readonly ServiceSkjemaService _serviceSkjemaService;
        /// <summary>
        /// The iris service
        /// </summary>
        private readonly INotyfService _irisService;


        /// <summary>
        /// Initializes a new instance of the <see cref="FunksjonsTestController" /> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        /// <param name="serviceSkjemaService">A service for handling service schematics operations.</param>
        /// <param name="irisService">A notification service used for user communication.</param>
        public FunksjonsTestController(AppDbContext context, ServiceSkjemaService serviceSkjemaService, INotyfService irisService)
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
        // GET: FunksjonsTest
        public async Task<IActionResult> Index()
        {
            // Retrieves all Mechanical entities and provides them to the Index view.
            return View(await _context.Mechanicals.ToListAsync());
        }



        /// <summary>
        /// Asynchronously retrieves and displays the details of a specific FunksjonsTest entity.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity to be displayed. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Details view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Details view for the FunksjonsTest entity is displayed.
        /// </returns>
        // GET: Mechanical/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the ID is provided, if not, return a NotFound result.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the FunksjonsTest entity by the provided ID asynchronously.
            var funksjonsTest = await _context.FunksjonsTests
                .FirstOrDefaultAsync(m => m.Id == id);

            // Check if an entity with the given ID was found, if not, return NotFound.
            if (funksjonsTest == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Details view with the entity data.
            return View(funksjonsTest);
        }

        /// <summary>
        /// Prepares the Create view for a new FunksjonsTest entity, pre-populated with data from an associated service order.
        /// </summary>
        /// <param name="serviceOrderId">The ID of the service order to associate with the new FunksjonsTest entity.</param>
        /// <param name="category">The category of the service order, with a default value of "FunksjonsTest".</param>
        /// <returns>
        /// If the service order is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Create view is returned with the FunksjonsTestListViewModel pre-populated with the service order data.
        /// </returns>
        /// <remarks>
        /// The method also checks for a success message from previous operations stored in TempData and
        /// uses the notification service to display it.
        /// </remarks>
        // GET: FunksjonsTest/Create
        [HttpGet]
        [Route("FunksjonsTest/Create/{serviceOrderId}/{category}")]
        public IActionResult Create(int serviceOrderId, string category = "FunksjonsTest")
        {
            // Check for a success message from TempData and display it using the notification service.
            if (TempData["SuccessMessageElektro"] != null)
            {
                _irisService.Success("SuccessMessageElektro", 3);
            }

            // Find the service order by ID.
            var serviceOrder = _context.ServiceOrders.Find(serviceOrderId);

            // If the service order with the given ID doesn't exist, return NotFound (HTTP 404).
            if (serviceOrder == null)
            {
                return NotFound();
            }

            // Create and populate the view model with data from the service order.
            var viewModel = new FunksjonsTestListViewModel
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
        /// Processes the submitted data for creating new FunksjonsTest records.
        /// </summary>
        /// <param name="viewModel">The view model containing the data for new FunksjonsTest records.</param>
        /// <param name="serviceOrderId">The ID of the service order associated with the new FunksjonsTest records.</param>
        /// <returns>
        /// If the model state is valid and the records are created successfully, redirects to the Create action of the Trykk controller.
        /// If no last service order is found, adds a model error.
        /// If the model state is not valid, re-displays the view with the current viewModel and error messages.
        /// </returns>
        /// <remarks>
        /// The method ensures that the first FunksjonsTest in the list carries the comment over from the view model.
        /// A success message is displayed after the records are saved.
        /// </remarks>
        // POST: FunksjonsTest/Create
        [HttpPost]
        [Route("FunksjonsTest/Create/{serviceOrderId}/{category}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FunksjonsTestListViewModel viewModel, int serviceOrderId)
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
                    foreach (var funksjonsTest in viewModel.FunksjonsTests)
                    {
                        // If this is the first iteration, copy the comment from the view model.

                        if (isFirst)
                        {
                            funksjonsTest.Kommentar = viewModel.Kommentar;
                            isFirst = false;
                        }

                        // Set the order number and service order ID for each FunksjonsTest.
                        funksjonsTest.Ordrenummer = lastServiceOrder.Ordrenummer;
                        funksjonsTest.ServiceOrderId = lastServiceOrder.ServiceOrderId;

                        // Add the new FunksjonsTest to the context.
                        _context.Add(funksjonsTest);
                    }
                    // Save all changes to the database.
                    await _context.SaveChangesAsync();
                    // Display a success message.
                    _irisService.Success("Listen ble lagret med suksess.", 3);

                    // Update the service schema status.
                    await UpdateServicejemaIfAllCompleted(serviceOrderId);

                    // Redirect to the Create action of the Trykk controller with the appropriate parameters.
                    return RedirectToAction("Create", "Trykk", new { serviceOrderId = viewModel.ServiceOrderId, category = "Trykk" });
                }
                else
                {
                    // If the model state is not valid, collect the error messages to display.
                    ModelState.AddModelError(string.Empty, "ServiceOrder not found.");
                }
            }
            // Return the view with the provided view model, including any errors.
            else
            {
                foreach (var modelState in ModelState)
                {
                    var fieldName = modelState.Key;
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Field: {fieldName}, Error: {error.ErrorMessage}");
                    }
                }
                // If the model state is not valid, collect the error messages to display.
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }

            // Return the view with the provided view model, including any errors.
            return View(viewModel);
        }

        /// <summary>
        /// Asynchronously retrieves a FunksjonsTest entity for editing.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity to be edited. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Edit view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Edit view for the FunksjonsTest entity is displayed.
        /// </returns>
        // GET: FunksjonsTest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the ID is provided, if not, return a NotFound result.
            if (id == null)
            {
                return NotFound();
            }
            // Attempt to find the FunksjonsTest entity by the provided ID asynchronously.
            var funksjonsTest = await _context.FunksjonsTests.FindAsync(id);

            // Check if an entity with the given ID was found, if not, return NotFound.
            if (funksjonsTest == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Edit view with the entity data.
            return View(funksjonsTest);
        }

        /// <summary>
        /// Asynchronously processes the submitted edit form for a FunksjonsTest entity.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity being edited.</param>
        /// <param name="funksjonsTest">The FunksjonsTest entity that has been bound from the edit form. The properties bound are 'Id', 'ChecklistItem', 'OK', 'BorSkiftes', and 'Defekt'.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult.
        /// If the id does not match the entity's ID, NotFound (HTTP 404) is returned.
        /// If the model state is valid, the changes are saved and the method redirects to the Index action.
        /// If there is a concurrency error and the FunksjonsTest entity no longer exists, NotFound is returned.
        /// Otherwise, the Edit view is re-displayed with the current FunksjonsTest entity.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency error is encountered while saving the FunksjonsTest entity.</exception>
        // POST: FunksjonsTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChecklistItem,OK,BorSkiftes,Defekt")] FunksjonsTest funksjonsTest)
        {
            // Check if the ID in the URL matches the entity's ID.
            if (id != funksjonsTest.Id)
            {
                return NotFound();
            }

            // If the model state is valid, attempt to update and save the entity.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funksjonsTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the FunksjonsTest entity still exists.
                    if (!FunksjonsTestExists(funksjonsTest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw the concurrency exception if the FunksjonsTest entity exists.
                        throw;
                    }
                }
                // If successful, redirect to the Index action.
                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, re-display the Edit view with the current FunksjonsTest entity.
            return View(funksjonsTest);
        }


        /// <summary>
        /// Asynchronously retrieves a FunksjonsTest entity to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity to delete. If null, the method returns a NotFound result.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the IActionResult for the Delete confirmation view.
        /// If the entity with the specified ID is not found, NotFound (HTTP 404) is returned.
        /// Otherwise, the Delete confirmation view for the FunksjonsTest entity is displayed.
        /// </returns>
        // GET: FunksjonsTest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate the ID is not null.
            if (id == null)
            {
                return NotFound();
            }

            // Attempt to find the FunksjonsTest entity by the provided ID asynchronously.
            var funksjonsTest = await _context.FunksjonsTests
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no entity with the given ID is found, return NotFound.
            if (funksjonsTest == null)
            {
                return NotFound();
            }

            // If an entity is found, return the Delete confirmation view with the entity data.
            return View(funksjonsTest);
        }

        /// <summary>
        /// Asynchronously deletes the specified FunksjonsTest entity from the database.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult that redirects to the Index action upon successful deletion.
        /// </returns>
        /// <remarks>
        /// This POST action is confirmed via the ActionName attribute and is protected against Cross-Site Request Forgery (CSRF) attacks using the ValidateAntiForgeryToken attribute.
        /// </remarks>
        // POST: FunksjonsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the FunksjonsTest entity by ID asynchronously.
            var funksjonsTest = await _context.FunksjonsTests.FindAsync(id);

            // Remove the found entity from the database context.
            _context.FunksjonsTests.Remove(funksjonsTest);

            // Save the changes to the database asynchronously.
            await _context.SaveChangesAsync();

            // Redirect to the Index action of the controller.
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a FunksjonsTest entity with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the FunksjonsTest entity to check for existence.</param>
        /// <returns>
        /// True if the entity exists; otherwise, false.
        /// </returns>
        private bool FunksjonsTestExists(int id)
        {
            // Use LINQ to check for the existence of the FunksjonsTest entity with the given ID.

            return _context.FunksjonsTests.Any(e => e.Id == id);
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
