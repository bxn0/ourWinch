using Microsoft.EntityFrameworkCore;
using ourWinch.Models;

namespace ourWinch.Utility
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        // DbSet özellikleri ile veritabanı tablolarını tanımlayın
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Checkpoints> Checkpoints { get; set; }
        public DbSet<InspectionForm> InspectionForms { get; set; }
        public DbSet<PressureSettings> PressureSettings { get; set; }

        // Veritabanı tablolarını temsil eden diğer DbSet özelliklerini ekleyebilirsiniz

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PressureSettings>()
                .HasOne(p => p.InspectionForm)
                .WithOne(f => f.PressureSetting)
                .HasForeignKey<PressureSettings>(p => p.FormId);
        }
    }
}
