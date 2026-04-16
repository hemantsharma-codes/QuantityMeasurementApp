using BCrypt.Net;
using BusinessLayer.Helpers;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs.Auth;
using ModelLayer.Entity;
using RepoLayer.Interfaces;

namespace BusinessLayer.Services
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepo;
    private readonly JwtService _jwtService;

    public AuthService(IUserRepository userRepo, JwtService jwtService)
    {
      _userRepo = userRepo;
      _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
      if (string.IsNullOrWhiteSpace(dto.Username))
        throw new ArgumentException("Username is required");

      if (string.IsNullOrWhiteSpace(dto.Email))
        throw new ArgumentException("Email is required");

      if (string.IsNullOrWhiteSpace(dto.Password))
        throw new ArgumentException("Password is required");

      if (await _userRepo.ExistsAsync(dto.Email))
        throw new InvalidOperationException("Email already registered.");

      var user = new User
      {
        Username = dto.Username.Trim(),
        Email = dto.Email.Trim().ToLower(),
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        Role = "User",
        CreatedAt = DateTime.UtcNow
      };

      await _userRepo.AddAsync(user);

      var (token, expiresAt) = _jwtService.GenerateToken(user);
      return BuildResponse(user, token, expiresAt);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
    {
      var user = await _userRepo.GetByEmailAsync(dto.Email)
          ?? throw new UnauthorizedAccessException("Invalid email or password.");

      if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        throw new UnauthorizedAccessException("Invalid email or password.");

      var (token, expiresAt) = _jwtService.GenerateToken(user);
      return BuildResponse(user, token, expiresAt);
    }

    public async Task UpdatePasswordAsync(int userId, UpdatePasswordDto dto)
    {
      if (dto.NewPassword != dto.ConfirmNewPassword)
        throw new ArgumentException("New password and confirm password do not match.");

      var user = await _userRepo.GetByIdAsync(userId)
          ?? throw new KeyNotFoundException("User not found.");

      if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
        throw new UnauthorizedAccessException("Current password is incorrect.");

      user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
      await _userRepo.UpdateAsync(user);
    }

    public async Task UpdateRoleAsync(UpdateRoleDto dto)
    {
      var role = dto.NewRole.Trim();

      if (!new[] { "User", "Admin" }.Contains(role, StringComparer.OrdinalIgnoreCase))
        throw new ArgumentException("Role must be 'User' or 'Admin'.");

      var user = await _userRepo.GetByEmailAsync(dto.Email.Trim().ToLower())
          ?? throw new KeyNotFoundException("User not found.");

      user.Role = role;
      await _userRepo.UpdateAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      return await _userRepo.GetAllAsync();
    }

    // ─── Helper ───────────────────────────────────────────────────────────────
    private static AuthResponseDto BuildResponse(User user, string token, DateTime expiresAt) => new()
    {
      Token = token,
      Username = user.Username,
      Email = user.Email,
      Role = user.Role,
      ExpiresAt = expiresAt
    };

    public async Task<User?> GetUserByIdAsync(int userId)
    {
      return await _userRepo.GetByIdAsync(userId);  // already exists in UserRepository
    }
  }
}