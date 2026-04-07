using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs.Auth
{
  public class UpdateRoleDto
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression("User|Admin", ErrorMessage = "Role must be User or Admin")]
    public string NewRole { get; set; }
  }
}