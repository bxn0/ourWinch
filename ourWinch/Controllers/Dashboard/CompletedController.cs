// Controllers/FulførteController.cs
using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;





[Authorize]

public class CompletedController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/Completed.cshtml");
    }
}