using BusinessLayer.Converters;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Helpers
{
  public static class UnitParser
  {
    public static (Enum unitEnum, IMeasurable measurable, string category) Parse(string quantityType, string unit)
    {
      if (string.IsNullOrWhiteSpace(quantityType) || string.IsNullOrWhiteSpace(unit))
        throw new ArgumentException("QuantityType and Unit are required.");

      switch (quantityType.Trim().ToLower())
      {
        case "length":
          if (!Enum.TryParse<LengthUnit>(unit, true, out var l))
            throw new ArgumentException("Invalid Length Unit");
          return (l, new LengthUnitConverter(l), "Length");

        case "weight":
          if (!Enum.TryParse<WeightUnit>(unit, true, out var w))
            throw new ArgumentException("Invalid Weight Unit");
          return (w, new WeightUnitConverter(w), "Weight");

        case "temperature":
          if (!Enum.TryParse<TemperatureUnit>(unit, true, out var t))
            throw new ArgumentException("Invalid Temperature Unit");
          return (t, new TemperatureUnitConverter(t), "Temperature");

        case "volume":
          if (!Enum.TryParse<VolumeUnit>(unit, true, out var v))
            throw new ArgumentException("Invalid Volume Unit");
          return (v, new VolumeUnitConverter(v), "Volume");

        default:
          throw new ArgumentException("Invalid Quantity Type");
      }
    }
  }
}