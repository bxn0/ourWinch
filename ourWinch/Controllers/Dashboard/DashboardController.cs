﻿// Controllers/DashboardController.cs
using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;



[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private const int PageSize = 5; 
    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    
    public IActionResult Index(int page = 1)
    {
        if (TempData["SuccessMessage"] != null)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
        }

        var message = TempData["LoginSuccessMessage"] as string;
        // Hvis meldingen ikke er null, send denne meldingen for å se den
        if (!string.IsNullOrEmpty(message))
        {
            ViewBag.LoginSuccessMessage = message;
        }


        var totalItems = _context.ServiceOrders.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        page = Math.Clamp(page, 1, totalPages);

        var serviceOrders = _context.ServiceOrders
            .OrderByDescending(cs => cs.MottattDato)
        .Skip((page - 1) * PageSize)
        .Take(PageSize)
        .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View("~/Views/Dashboard/ActiveService.cshtml", serviceOrders);

    }


}