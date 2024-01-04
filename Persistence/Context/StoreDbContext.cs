using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions options) : base(options)
    {
    }
    /*
    public StoreDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your Connection String");
    }*/
    public DbSet<Product> Products { get; set; }
}