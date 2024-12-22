using Microsoft.EntityFrameworkCore;

namespace EnergyPriceChecker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<EnergyPrice> EnergyPrices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnergyPrice>()
            .HasKey(b => b.PriceId)
            .HasName("PrimaryKey_PriceId");
    }
}

