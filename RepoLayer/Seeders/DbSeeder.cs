using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelLayer.Entity;
using RepoLayer.Data;

namespace RepoLayer.Seeders
{
  /// <summary>
  /// Runs after migration to ensure the default Admin user exists.
  /// Uses BCrypt to hash the password at runtime — avoids hardcoded hash issues.
  /// </summary>
  public static class DbSeeder
  {
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

      // Apply any pending migrations first
      await db.Database.MigrateAsync();

      // Check if admin already exists
      bool adminExists = await db.Users.AnyAsync(u => u.Email == "admin@quantityapp.com");
      if (adminExists) return;

      var admin = new User
      {
        Username = "admin",
        Email = "admin@quantityapp.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123", 11),
        Role = "Admin",
        CreatedAt = DateTime.UtcNow
      };

      await db.Users.AddAsync(admin);
      await db.SaveChangesAsync();

      Console.WriteLine(" Admin user seeded: admin@quantityapp.com / Admin@123");
    }
  }
}