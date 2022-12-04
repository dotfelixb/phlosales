using Microsoft.EntityFrameworkCore;
using PhloSales.Data.Entities;
using System.Security.Principal;

namespace PhloSales.Data;

public class PhloSalesDbContext : DbContext
{
    public PhloSalesDbContext(DbContextOptions<PhloSalesDbContext> options) : base(options)
    {
    }

    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}

