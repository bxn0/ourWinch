using Microsoft.EntityFrameworkCore;
using ourWinch.Models.Dashboard;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceOrder> ServiceOrders { get; set; }
    public DbSet<ActiveService> ActiveServices { get; set; }
    public DbSet<Mechanical> Mechanicals { get; set; }
    public DbSet<Electro> Electros { get; set; }
    public DbSet<Hydrolisk> Hydrolisks { get; set; }
    public DbSet<FunksjonsTest> FunksjonsTests { get; set; }
    public DbSet<Trykk> Trykks { get; set; }
}
