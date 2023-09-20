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
            if (model.Email == "test@test.com" && model.Password == "12345")
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
}
