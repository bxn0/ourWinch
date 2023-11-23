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
    ///

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
        /// Initializes a new instance of the <see cref="RolesController"/> class.
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
        /// <returns>The index view populated with a list of roles.</returns>
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();

            return View(roles);
        }


        [HttpGet]
        public IActionResult Upsert(string id)
        {

            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                //updating the role
                var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == id);
                return View(objFromDb);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (await _roleManager.RoleExistsAsync(roleObj.Name))
            {
                //error
                _irisService.Error("Rollen eksisterer allerede!", 3);
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrEmpty(roleObj.Id))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                _irisService.Success("Rollen ble lagt!", 3);

            }
            else
            {
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
        [HttpPost]
        //[Authorize(Policy = "OnlySuperAdminChecker")]
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
