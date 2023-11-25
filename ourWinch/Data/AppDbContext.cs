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
    /// Initializes a new instance of the <see cref="AppDbContext" /> class.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Constructor logic.
    }

    /// <summary>
    /// Gets or sets the DbSet for ServiceOrders.
    /// </summary>
    /// <value>
    /// The service orders.
    /// </value>
    public DbSet<ServiceOrder> ServiceOrders { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for ActiveServices.
    /// </summary>
    /// <value>
    /// The active services.
    /// </value>
    public DbSet<ActiveService> ActiveServices { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for CompletedServices.
    /// </summary>
    /// <value>
    /// The completed services.
    /// </value>
    public DbSet<CompletedService> CompletedServices { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Mechanicals.
    /// </summary>
    /// <value>
    /// The mechanicals.
    /// </value>
    public DbSet<Mechanical> Mechanicals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Electros.
    /// </summary>
    /// <value>
    /// The electros.
    /// </value>

    public DbSet<Electro> Electros { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Hydrolisks.
    /// </summary>
    /// <value>
    /// The hydrolisks.
    /// </value>

    public DbSet<Hydrolisk> Hydrolisks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for FunksjonsTests.
    /// </summary>
    /// <value>
    /// The funksjons tests.
    /// </value>

    public DbSet<FunksjonsTest> FunksjonsTests { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Trykks.
    /// </summary>
    /// <value>
    /// The trykks.
    /// </value>

    public DbSet<Trykk> Trykks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for ApplicationUser.
    /// </summary>
    /// <value>
    /// The application user.
    /// </value>
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

}
