﻿using Microsoft.EntityFrameworkCore;
using ourWinch.Models;
using System.Collections.Generic;


// Utility veritabani ile etnititiler arasinda kopru kurar asp. mekanizmasina gore
namespace ourWinch.Utility
{
    public class ApplicationDBContext : DbContext
    {
        internal object Brukere;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) { }

        public DbSet<Bruker> Bruker { get; set; }
        public DbSet<Kunde> Kunde {  get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<Service> Service{ get; set; }

        public DbSet<ServisType> ServisType { get; set; }

        public DbSet<SjekkListe> SjekkListe { get; set; }

    }
}

