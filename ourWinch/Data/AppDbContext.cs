﻿using Microsoft.EntityFrameworkCore;
using ourWinchSist.Models; // Bu satırı ekleyerek ServiceOrder sınıfını dahil ediyoruz.

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceOrder> ServiceOrders { get; set; }
}