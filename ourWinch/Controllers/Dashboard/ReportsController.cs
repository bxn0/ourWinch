using ourWinch.Models.Dashboard;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller for handling report-related operations in an MVC application.
/// This controller is accessible only to authorized users.
/// </summary>
[Authorize]
public class ReportsController : Controller
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportsController" /> class.
    /// </summary>
    /// <param name="context">The database context to be used by this controller.</param>
    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays the dashboard for reports.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult" /> that renders the reports dashboard view.
    /// </returns>
    public IActionResult Dashboard()
    {
        // Return the view for the reports dashboard.
        return View("~/Views/Dashboard/Reports.cshtml");
    }
}