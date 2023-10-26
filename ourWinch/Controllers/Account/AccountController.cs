using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;

public class AccountController : Controller
{
    private static string VerificationCode = "123456"; // Bu kısmı JavaScript'te belirttiğiniz değere göre güncelledim.

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpGet]
    public async Task<IActionResult> Register()
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();

        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.MobilNo, Fornavn = model.Fornavn, Etternavn = model.Etternavn,
                MellomNavn = model.MellomNavn, Password = model.Password
            };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Dashboard");
            }
            AddErrors(result);
           

        }

        return View(model);
    }


    [HttpPost]
    public IActionResult SendCode(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            ViewBag.CodeSent = true;
        }
        return View("ForgotPassword", model);
    }

    [HttpPost]
    public IActionResult VerifyCode(string code)
    {
        if (code == VerificationCode)
        {
            return RedirectToAction("ResetPassword");
        }
        ViewBag.ErrorMessage = "VerifyCode Wrong!";
        return View("ResetPassword");
    }


    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Login");
        }
        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Mobilno == "123456" && model.Password == "password") // Burası JavaScript kodu ile uyumlu.
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Feil mobil nummer eller password! Password må være på minst 8 tegn! Prøv igjen!";
            }
        }
        return View(model);
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty,error.Description);
        }
    }
}
