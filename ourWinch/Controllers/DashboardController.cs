// Controllers/DashboardController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        DashboardModel model = new DashboardModel();
        // model'in özelliklerini doldurabilirsiniz eğer gerekliyse
        return View(model);
    }

}
