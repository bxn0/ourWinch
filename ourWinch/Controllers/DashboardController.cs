// Controllers/DashboardController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
