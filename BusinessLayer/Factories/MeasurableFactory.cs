using BusinessLayer.Converters;
using BusinessLayer.Services;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Factories
{
  public static class MeasurableFactory
  {
    public static IMeasurable GetMeasurable<U>(U unit) where U : Enum
    {
      if (unit is LengthUnit l)
      {
        return new LengthUnitConverter(l);
      }
      else if (unit is TemperatureUnit t)
      {
        return new TemperatureUnitConverter(t);
      }
      else if (unit is VolumeUnit v)
      {
        return new VolumeUnitConverter(v);
      }
      else if (unit is WeightUnit w)
      {
        return new WeightUnitConverter(w);
      }

      throw new InvalidOperationException("Unsupported unit type");
    }
  }
}