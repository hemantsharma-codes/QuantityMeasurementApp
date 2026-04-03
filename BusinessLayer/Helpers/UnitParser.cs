using BusinessLayer.Converters;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Helpers
{
  public static class UnitParser
  {
    public static (Enum unitEnum, IMeasurable measurable, string category) Parse(string quantityType, string unit)
    {
      switch (quantityType.ToLower())
      {
        case "length":
          var l = Enum.Parse<LengthUnit>(unit, true);
          return (l, new LengthUnitConverter(l), "Length");

        case "weight":
          var w = Enum.Parse<WeightUnit>(unit, true);
          return (w, new WeightUnitConverter(w), "Weight");

        case "temperature":
          var t = Enum.Parse<TemperatureUnit>(unit, true);
          return (t, new TemperatureUnitConverter(t), "Temperature");

        case "volume":
          var v = Enum.Parse<VolumeUnit>(unit, true);
          return (v, new VolumeUnitConverter(v), "Volume");

        default:
          throw new Exception("Invalid Quantity Type");
      }
    }
  }
}