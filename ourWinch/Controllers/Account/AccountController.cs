using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;



public class AccountController : Controller
{
    

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();

        return View(registerViewModel);
    }


    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.MobilNo, Fornavn = model.Fornavn, Etternavn = model.Etternavn,
                MellomNavn = model.MellomNavn
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
    [HttpGet]
    public  IActionResult Login()
    {
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Mobilno, model.Password, model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Invalid login attempt. ");
                return View(model);
            }
            


        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Logout()
    {

       await _signInManager.SignOutAsync();
       return RedirectToAction("Login", "Account");
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



    //[HttpPost]
    //public IActionResult VerifyCode(string code)
    //{
    //    if (code == VerificationCode)
    //    {
    //        return RedirectToAction("ResetPassword");
    //    }
    //    ViewBag.ErrorMessage = "VerifyCode Wrong!";
    //    return View("ResetPassword");
    //}


    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Login");
        }
        return View(model);
    }

    

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    /*

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

    
    */
}
