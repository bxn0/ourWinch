using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        // Giriş işlemleri burada gerçekleştirilir.
        if (ModelState.IsValid)
        {
            // Örnek olarak:
            if (model.Mobil == "test@test.com" && model.Password == "12345")
            {
                return RedirectToAction("Index", "Home"); // Başarılı giriş sonrası yönlendirme
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz giriş denemesi.");
            }
        }

        return View(model);
    }

    //! Glemt Passord

    [HttpGet]
    public IActionResult GlemtPassord()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendCode(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Simüle edilmiş kod gönderme. Gerçekte bir SMS API'si kullanarak telefon numarasına bir kod gönderilir.
            TempData["VerificationCode"] = "1234"; // Örnek olarak sabit bir kod kullandım.
            TempData["UserMobil"] = model.Mobil;
            return RedirectToAction("VerifyCode");
        }
        return View("GlemtPassord", model);
    }

    [HttpGet]
    public IActionResult VerifyCode()
    {
        return View();
    }

    [HttpPost]
    public IActionResult VerifyCode(string code)
    {
        if (code == TempData["VerificationCode"].ToString())
        {
            return RedirectToAction("ResetPassword");
        }
        ModelState.AddModelError("", "Feil kode. Vennligst prøv igjen.");
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            // TODO: Kullanıcının şifresini güncelleyin.
            return RedirectToAction("Login");
        }
        return View(model);
    }
} // Bu satırdaki süslü parantezi kaldırdım.
