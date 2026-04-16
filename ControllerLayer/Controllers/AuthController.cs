using System.Security.Claims;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs.Auth;

namespace ControllerLayer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    // POST api/auth/register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = await _authService.RegisterAsync(dto);
      return Ok(result);
    }

    // POST api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
      try
      {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
      }
      catch (UnauthorizedAccessException ex)
      {
        return Unauthorized(new { message = ex.Message });
      }
    }

    // GET api/auth/me  (logged-in user's profile)
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
      int userId = GetCurrentUserId();
      var user = await _authService.GetUserByIdAsync(userId);
      if (user == null) return NotFound(new { message = "User not found." });
      return Ok(new { user.Id, user.Username, user.Email, user.Role, user.CreatedAt });
    }

    // PUT api/auth/update-password  (logged-in user)
    [HttpPut("update-password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
    {
      try
      {
        int userId = GetCurrentUserId();
        await _authService.UpdatePasswordAsync(userId, dto);
        return Ok(new { message = "Password updated successfully." });
      }
      catch (UnauthorizedAccessException ex) { return Unauthorized(new { message = ex.Message }); }
      catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
      catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    // PUT api/auth/update-role  (Admin only)
    [HttpPut("update-role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto dto)
    {
      try
      {
        await _authService.UpdateRoleAsync(dto);
        return Ok(new { message = $"Role updated to '{dto.NewRole}' for {dto.Email}." });
      }
      catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
      catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }

    // GET api/auth/users  (Admin only)
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
      var users = await _authService.GetAllUsersAsync();
      var result = users.Select(u => new
      {
        u.Id,
        u.Username,
        u.Email,
        u.Role,
        u.CreatedAt
      });
      return Ok(result);
    }

    // ─── Helper ───────────────────────────────────────────────────────────────
    private int GetCurrentUserId()
    {
      var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
          ?? throw new UnauthorizedAccessException("User ID not found in token.");
      return int.Parse(claim);
    }
  }
}