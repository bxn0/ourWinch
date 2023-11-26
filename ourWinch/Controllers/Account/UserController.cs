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
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
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
        /// Initializes a new instance of the <see cref="UserController" /> class.
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


        /// <summary>
        /// Displays the index view with a list of all users and their associated roles.
        /// Retrieves users from the database, determines each user's role, and passes the data to the view.
        /// If a user does not have an assigned role, 'None' is set as their role.
        /// </summary>
        /// <returns>
        /// The index view populated with a list of users and their roles.
        /// </returns>
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


        /// <summary>
        /// Displays the edit view for a specific user based on their ID.
        /// If the user is not found, returns a 'NotFound' response.
        /// The method also prepares and assigns a list of roles for display in a dropdown in the view.
        /// </summary>
        /// <param name="userId">The ID of the user to be edited.</param>
        /// <returns>
        /// The 'Edit' view populated with the user's details and a list of roles.
        /// Returns 'NotFound' if the user does not exist.
        /// </returns>
        public IActionResult Edit(string userId)
        {

            // Attempt to find the user in the database based on the provided userId.
            var objFromDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);

            // If no user is found with the provided ID, return a NotFound result (404 error).
            if (objFromDB == null)
            {
                return NotFound();
            }

            // Retrieve all associations of users with roles.
            var userRole = _db.UserRoles.ToList();

            // Retrieve all role definitions.
            var roles = _db.Roles.ToList();

            // Find the role associated with the user, if any.
            var role = userRole.FirstOrDefault(u => u.UserId == userId);


            // If the user has a role assigned, set the RoleId in the objFromDB to the corresponding role's ID.
            if (role != null)
            {
                objFromDB.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }


            // Prepare a list of roles in the format required for a dropdown list in the view.
            // Each role is represented as a SelectListItem with its Text and Value set
            objFromDB.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id,
            });
            // Return the 'Edit' view, providing the user's details including their role and the list of roles for a dropdown.
            return View(objFromDB);
        }




        /// <summary>
        /// Processes the submission for editing a user's details and role.
        /// If the user is not found, a 'NotFound' response is returned.
        /// The method updates the user's role and other details in the database.
        /// </summary>
        /// <param name="user">The user object containing the updated details.</param>
        /// <returns>
        /// A redirection to the index view on successful edit,
        /// or the edit view with the updated user data if the model state is not valid.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {

            // Validate the model state.
            if (ModelState.IsValid)
            {

                // Retrieve the user from the database based on the provided user ID.
                var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == user.Id);

                // If no user is found, return a NotFound result.
                if (objFromDb == null)
                {
                    return NotFound();
                }

                // Check if the user has an existing role.
                var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == objFromDb.Id);
                if (userRole != null)
                {

                    // Retrieve the name of the user's current role.
                    var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    //removing the old role
                    await _userManager.RemoveFromRoleAsync(objFromDb, previousRoleName);

                }

                //add new role
                await _userManager.AddToRoleAsync(objFromDb, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);

                // Update the user's details in the database.
                objFromDb.Fornavn = user.Fornavn;

                //save changes
                _db.SaveChanges();

                // Notify success and redirect to the index view.
                _irisService.Success("Brukeren ble redigert!",3);
                return RedirectToAction(nameof(Index));
            }

            // Prepare the role list for the dropdown if the model state is not valid.
            user.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });

            // Return the view with the user data for further editing.
            return View(user);
        }


        /// <summary>
        /// Handles the deletion of a user based on their ID.
        /// If the user is not found, a 'NotFound' response is returned.
        /// Otherwise, the user is removed from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>
        /// A redirection to the index view on successful deletion,
        /// or a 'NotFound' response if the user does not exist.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(string userId)
        {

            // Attempt to find the user in the database based on the provided userId.
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);

            // If no user is found with the provided ID, return a NotFound result.
            if (objFromDb == null)
            {
                return NotFound();
            }

            // Remove the user from the database.
            _db.ApplicationUser.Remove(objFromDb);

            //save the data
            _db.SaveChanges();

            //notify
          _irisService.Success("Brukeren ble sletted!", 2);
            return RedirectToAction(nameof(Index));
        }



    }
}
