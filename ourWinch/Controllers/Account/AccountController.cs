using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private static string VerificationCode = "123456"; // Bu kısmı JavaScript'te belirttiğiniz değere göre güncelledim.

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
            if (model.Mobil == "123456" && model.Password == "password") // Burası JavaScript kodu ile uyumlu.
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

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Account");
    }


    public IActionResult ForgotPassword()
    {
        return View();
    }
   
}
