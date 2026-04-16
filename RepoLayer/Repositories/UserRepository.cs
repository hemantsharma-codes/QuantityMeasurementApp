using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using RepoLayer.Data;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
      return await _context.Users
          .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(User user)
    {
      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email)
    {
      return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }
  }
}