using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs.Auth
{
  public class RegisterRequestDto
  {
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$",
      ErrorMessage = "Password must contain at least one letter and one number")]
    public string Password { get; set; }
  }
}