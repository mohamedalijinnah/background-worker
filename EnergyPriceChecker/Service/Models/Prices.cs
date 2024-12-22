using EnergyPriceChecker.Data;

namespace EnergyPriceChecker.Service.Models;

public class Prices
{
    public decimal Price { get; set; }
    public string ReadingDate { get; set; }

    public EnergyPrice ToEnergyPrice(UsageType energyType)
    {
        return new EnergyPrice()
        {
            EnergyType = energyType.ToString(),
            Price = Price,
            ReadingDate = ReadingDate
        };
    }
}
