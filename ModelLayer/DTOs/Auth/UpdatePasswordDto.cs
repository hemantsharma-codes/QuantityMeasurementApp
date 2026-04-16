using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs.Auth
{
  public class UpdatePasswordDto
  {
    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmNewPassword { get; set; }
  }
}