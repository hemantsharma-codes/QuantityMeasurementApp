using ModelLayer.Entity;

namespace RepoLayer.Interfaces
{
  public interface IUserRepository
  {
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> ExistsAsync(string email);
  }
}