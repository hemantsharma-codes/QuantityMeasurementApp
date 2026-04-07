namespace ModelLayer.Interfaces
{
  public interface IMeasurable
  {
    double ConvertFromBase(double value);
    double ConvertToBase(double baseValue);
    string GetSymbol();
  }
}