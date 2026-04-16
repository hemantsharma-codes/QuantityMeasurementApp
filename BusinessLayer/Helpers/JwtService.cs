using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Entity;

namespace BusinessLayer.Helpers
{
  public class JwtService
  {
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

    public JwtService(IConfiguration config)
    {
      _secretKey = config["JwtSettings:SecretKey"] ?? throw new Exception("JWT SecretKey not configured");
      _issuer = config["JwtSettings:Issuer"] ?? "QuantityApp";
      _audience = config["JwtSettings:Audience"] ?? "QuantityAppUsers";
      _expiryMinutes = int.Parse(config["JwtSettings:ExpiryMinutes"] ?? "60");
    }

    public (string token, DateTime expiresAt) GenerateToken(User user)
    {
      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name,           user.Username),
        new Claim(ClaimTypes.Email,          user.Email),
        new Claim(ClaimTypes.Role,           user.Role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expiresAt = DateTime.UtcNow.AddMinutes(_expiryMinutes);

      var token = new JwtSecurityToken(
          issuer: _issuer,
          audience: _audience,
          claims: claims,
          expires: expiresAt,
          signingCredentials: creds
      );

      return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
  }
}