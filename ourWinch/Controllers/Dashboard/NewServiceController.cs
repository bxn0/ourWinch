// Controllers/Fulf�rteController.cs
using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class NewServiceController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/NewService.cshtml");
    }
}
