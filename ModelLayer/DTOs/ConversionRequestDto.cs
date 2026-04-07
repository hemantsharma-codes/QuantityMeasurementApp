using System.ComponentModel.DataAnnotations;

public class ConversionRequestDto
{
  [Required]
  public string QuantityType { get; set; }

  [Range(0, double.MaxValue)]
  public double Value { get; set; }

  [Required]
  public string SourceUnit { get; set; }

  [Required]
  public string TargetUnit { get; set; }
}