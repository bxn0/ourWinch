using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;

namespace ourWinch.Controllers.Account
{
    public class RolesController : Controller
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public RolesController(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

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
    }
}
