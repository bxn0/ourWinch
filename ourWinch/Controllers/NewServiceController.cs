// Controllers/FulførteController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class NewServiceController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/NewService.cshtml");
    }
}
