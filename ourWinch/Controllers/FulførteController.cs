// Controllers/FulførteController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class FulførteController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/Fulførte.cshtml");
    }
}
