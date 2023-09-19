using Microsoft.AspNetCore.Mvc;

using ourWinch.Models;
using ourWinch.Services;
using System.Data.SqlClient;

namespace ourWinch.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(model);
            }

            // Set the user in the session/claims and redirect to the desired page

            return RedirectToAction("Index", "Home");
        }
    }

}
