using AspNetCoreHero.ToastNotification.Abstractions;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;

namespace ourWinch.Controllers.Account
{

    /// <summary>
    /// Controller responsible for managing user-related operations within the application.
    /// This includes user management tasks such as creating, editing, and deleting user accounts.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// The database context used for data access operations related to users.
        /// </summary>
        private readonly AppDbContext _db;

        /// <summary>
        /// Manages user accounts for the application, including creation, deletion, and user role assignments.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Service for sending notifications, useful for informing users about user management operations.
        /// </summary>
        private readonly INotyfService _irisService;



        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// This constructor injects the database context, user manager, and notification service
        /// required for user management operations within the application.
        /// </summary>
        /// <param name="db">The database context used for user-related data operations.</param>
        /// <param name="userManager">The user manager for handling operations related to user accounts.</param>
        /// <param name="irisService">The notification service used for sending notifications related to user management.</param>
        public UserController(AppDbContext db, UserManager<ApplicationUser> userManager, INotyfService irisService)
        {
            _db = db;
            _userManager = userManager;
            _irisService = irisService;

        }

        public IActionResult Index()
        {
            var userList = _db.ApplicationUser.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }

        public IActionResult Edit(string userId)
        {
            var objFromDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if (objFromDB == null)
            {
                return NotFound();
            }
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == userId);
            if (role != null)
            {
                objFromDB.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }

            objFromDB.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id,
            });

            return View(objFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == user.Id);
                if (objFromDb == null)
                {
                    return NotFound();
                }
                var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == objFromDb.Id);
                if (userRole != null)
                {
                    var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    //removing the old role
                    await _userManager.RemoveFromRoleAsync(objFromDb, previousRoleName);

                }

                //add new role
                await _userManager.AddToRoleAsync(objFromDb, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                objFromDb.Fornavn = user.Fornavn;
                _db.SaveChanges();
                _irisService.Success("Brukeren ble redigert!",3);
                return RedirectToAction(nameof(Index));
            }


            user.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _db.ApplicationUser.Remove(objFromDb);
            _db.SaveChanges();
          _irisService.Success("Brukeren ble sletted!", 2);
            return RedirectToAction(nameof(Index));
        }



    }
}
