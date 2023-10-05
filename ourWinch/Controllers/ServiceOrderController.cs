using OurWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


public class ServiceOrderController : Controller
{

    public IActionResult Checklist()
    {
        return View();
    }

}