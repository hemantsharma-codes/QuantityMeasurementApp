using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

namespace RepoLayer.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<QuantityMeasurement> QuantityMeasurement { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Relationships
      modelBuilder.Entity<QuantityMeasurement>()
          .HasOne(q => q.User)
          .WithMany(u => u.Measurements)
          .HasForeignKey(q => q.UserId)
          .OnDelete(DeleteBehavior.Cascade); // when a user is deleted, their measurements are also deleted

      // Index on UserId for faster history queries
      modelBuilder.Entity<QuantityMeasurement>()
          .HasIndex(q => q.UserId)
          .HasDatabaseName("IX_QuantityMeasurement_UserId");

      // Unique index on Email
      modelBuilder.Entity<User>()
          .HasIndex(u => u.Email)
          .IsUnique()
          .HasDatabaseName("IX_Users_Email_Unique");

      // NOTE: Admin user is seeded at runtime via RepoLayer.Seeders.DbSeeder
      // This avoids hardcoded BCrypt hash issues across different machines.
    }
  }
}