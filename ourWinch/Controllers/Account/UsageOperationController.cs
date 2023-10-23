using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;
namespace ourWinch.Controllers.Account
{
    public class UsageOperationsController : Controller
    {
        public IActionResult Index()
        {
            var model = new RegistrationModel(); // Modelin yeni bir örneğini oluştur
            return View(model);
        }
    }
}