using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs
{
  public class QuantityDto<U> where U : Enum
  {
    [Required(ErrorMessage = "Value is required")]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Value must be positive")]
    public double Value { get; set; }

    [Required(ErrorMessage = "Unit is required")]
    public U Unit { get; set; }
  }
}