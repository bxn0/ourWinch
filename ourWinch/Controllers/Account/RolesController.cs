using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
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
        private readonly INotyfService _irisService;



        public RolesController(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, INotyfService irisService)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _irisService = irisService;
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
