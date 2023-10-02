// Controllers/FulførteController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class NyServiceController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/NyService.cshtml");
    }
}
