using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ourWinch;
using ourWinch.Models.Account;



public class AccountController : Controller
{
    

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly INotyfService _irisService;
    //private readonly IEmailSender _emailSender;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,  RoleManager<IdentityRole> roleManager, INotyfService irisService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
       // _emailSender = emailSender;
        _roleManager = roleManager;
        //notfity
        _irisService = irisService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Register()
    {

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            // create the role
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Ansatt"));
        }


        RegisterViewModel registerViewModel = new RegisterViewModel()
       ;

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
                UserName = model.Fornavn, Fornavn = model.Fornavn, Etternavn = model.Etternavn,
                MellomNavn = model.MellomNavn, Email = model.Email, PhoneNumber = model.MobilNo
            };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                if (model.Role!=null && model.Role.Length > 0 && model.Role=="Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Ansatt");
                }

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
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
               // TempData[SD.Success] = "Login Successfull!";
               // TempData[SD.Warning] = "Logged out!";
                //TempData["LoginSuccessMessage"] = "Vellykket innlogging!";
                _irisService.Success("Successfully Logged in", 1);
                return RedirectToAction("Index", "Dashboard");
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
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



    [HttpGet]
    public IActionResult ForgotPassword()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

          //  await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager",
            //    "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");

            return RedirectToAction("ForgotPasswordConfirmation");
        }


        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
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

    /*

    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Login");
        }
        return View(model);
    }
    */


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
