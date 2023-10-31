using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;

namespace ourWinch.Controllers.Account
{
    public class UserController : Controller
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

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

     
    }
}
