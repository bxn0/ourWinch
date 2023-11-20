using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Account;
using ourWinch.Models.Dashboard;

/// <summary>
/// Represents the database context for the application, derived from IdentityDbContext for user management.
/// </summary>
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Constructor logic.
    }

    /// <summary>
    /// Gets or sets the DbSet for ServiceOrders.
    /// </summary>
    public DbSet<ServiceOrder> ServiceOrders { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for ActiveServices.
    /// </summary>
    public DbSet<ActiveService> ActiveServices { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for CompletedServices.
    /// </summary>
    public DbSet<CompletedService> CompletedServices { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Mechanicals.
    /// </summary>
    public DbSet<Mechanical> Mechanicals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Electros.
    /// </summary>

    public DbSet<Electro> Electros { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Hydrolisks.
    /// </summary>

    public DbSet<Hydrolisk> Hydrolisks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for FunksjonsTests.
    /// </summary>

    public DbSet<FunksjonsTest> FunksjonsTests { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Trykks.
    /// </summary>

    public DbSet<Trykk> Trykks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for ApplicationUser.
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

}
