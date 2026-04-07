using System.ComponentModel.DataAnnotations;

public class DivideRequestDto
{
  [Required]
  public string QuantityType { get; set; }

  [Range(0, double.MaxValue)]
  public double Value1 { get; set; }

  [Range(0.000001, double.MaxValue, ErrorMessage = "Value2 cannot be zero")]
  public double Value2 { get; set; }

  [Required]
  public string Unit1 { get; set; }

  [Required]
  public string Unit2 { get; set; }
}