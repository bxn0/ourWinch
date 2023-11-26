using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;

namespace ourWinch.Controllers.Account
{

    /// <summary>
    /// Controller responsible for managing user roles within the application.
    /// This includes operations like creating, editing, and deleting roles.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />

    [Authorize]
    public class RolesController : Controller
    {
        /// <summary>
        /// The database context used for data access operations.
        /// </summary>
        private readonly AppDbContext _db;

        /// <summary>
        /// Manages user accounts for the application, particularly in relation to role assignment.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Manages roles within the application, including role creation and deletion.
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Service for sending notifications, useful for informing users about role management operations.
        /// </summary>
        private readonly INotyfService _irisService;


        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController" /> class.
        /// This constructor injects the database context, user manager, role manager, and notification service
        /// needed for role management operations within the application.
        /// </summary>
        /// <param name="db">The database context used for data operations related to roles.</param>
        /// <param name="userManager">The user manager for handling user-related operations, especially in relation to roles.</param>
        /// <param name="roleManager">The role manager for handling role creation, deletion, and assignment.</param>
        /// <param name="irisService">The notification service used for sending notifications related to role management.</param>
        public RolesController(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, INotyfService irisService)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _irisService = irisService;
        }


        /// <summary>
        /// Displays the index view with a list of all roles.
        /// Retrieves roles from the database and passes them to the view.
        /// </summary>
        /// <returns>
        /// The index view populated with a list of roles.
        /// </returns>
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();

            return View(roles);
        }


        /// <summary>
        /// Displays a view for creating a new role or updating an existing role.
        /// If no ID is provided, it returns a view for creating a new role.
        /// If an ID is provided, it fetches and returns a view for editing the corresponding role.
        /// </summary>
        /// <param name="id">The ID of the role to edit. If null or empty, the method assumes a new role is being created.</param>
        /// <returns>
        /// A view for creating a new role if the ID is null or empty,
        /// or a view for editing an existing role if an ID is provided.
        /// </returns>
        [HttpGet]
        public IActionResult Upsert(string id)
        {

            if (String.IsNullOrEmpty(id))
            {

                // Return the view for creating a new role.
                return View();
            }
            else
            {
                //updating the role
                var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == id);
                return View(objFromDb);
            }
            
        }


        /// <summary>
        /// Processes the submission for creating a new role or updating an existing role.
        /// If the role already exists, an error is displayed and redirected back to the index.
        /// If creating a new role, it adds the role to the database.
        /// If updating an existing role, it updates the role details in the database.
        /// </summary>
        /// <param name="roleObj">The role object containing the role's details.</param>
        /// <returns>
        /// A redirection to the index view after processing the request.
        /// If an error occurs or if the role already exists, it returns an error message and redirects to the index.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (await _roleManager.RoleExistsAsync(roleObj.Name))
            {
                // If the role already exists, show an error message.
                _irisService.Error("Rollen eksisterer allerede!", 3);
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrEmpty(roleObj.Id))
            {
                // If the role ID is null or empty, create a new role.
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                _irisService.Success("Rollen ble lagt!", 3);

            }
            else
            {

                // If updating an existing role, update its details.
                var objRoleFromDb = _db.Roles.FirstOrDefault(u => u.Id == roleObj.Id);
                if (objRoleFromDb==null)
                {
                    _irisService.Error("Rollen ble ikke funnet!", 3);
                    return RedirectToAction(nameof(Index));
                }
                objRoleFromDb.Name = roleObj.Name;
                objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(objRoleFromDb);
                _irisService.Success("Rollen oppdatert!", 3);
            }

            return RedirectToAction(nameof(Index));

        }



        /// <summary>
        /// Handles the deletion of a role.
        /// Checks if the role exists and whether there are users assigned to it.
        /// If the role does not exist or if there are users assigned to it, an appropriate message is displayed.
        /// If it's safe to delete, the role is removed from the database.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>
        /// A redirection to the index view after processing the request.
        /// If an error occurs or if deletion is not feasible, it returns an error/warning message and redirects to the index.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
              _irisService.Error("Rollen ble ikke Funnet",2);
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _db.UserRoles.Where(u => u.RoleId == id).Count();
            if (userRolesForThisRole > 0)
            {
                _irisService.Warning("Kan ikke slette rollen fordi det er brukere tildelt den!", 2);
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(objFromDb);
            _irisService.Success("Rollen ble sletted!", 2);
            return RedirectToAction(nameof(Index));

        }
    }
}
