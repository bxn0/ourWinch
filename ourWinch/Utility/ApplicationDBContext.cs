using Microsoft.EntityFrameworkCore;
using ourWinch.Models;


// Utility veritabani ile etnititiler arasinda kopru kurar asp. mekanizmasina gore
namespace ourWinch.Utility
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) { }  

        public DbSet<AktiveServiser> AktivServis {  get; set; }
        public DbSet<FulførteService> FulførtService { get; set; }
    }
}

