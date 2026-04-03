using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

namespace RepoLayer.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<QuantityMeasurement> QuantityMeasurement { get; set; }
  }
}