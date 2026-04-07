using ModelLayer.DTOs.Auth;
using ModelLayer.Entity;

namespace BusinessLayer.Interfaces
{
  public interface IAuthService
  {
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
    Task UpdatePasswordAsync(int userId, UpdatePasswordDto dto);
    Task UpdateRoleAsync(UpdateRoleDto dto);               // Admin only
    Task<IEnumerable<User>> GetAllUsersAsync();            // Admin only
    Task<User?> GetUserByIdAsync(int userId);
  }
}