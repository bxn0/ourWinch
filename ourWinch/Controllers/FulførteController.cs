// Controllers/FulførteController.cs
using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class FulførteController : Controller
{
    public IActionResult Fulførte()
    {
        return View(Fulførte);
    }
}
