using EnergyPriceChecker.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPriceChecker.Jobs.Abstract;

public abstract class EnergyJobCreator
{
    public abstract IEnergyJob FactoryMethod();

    public async Task<IReadOnlyCollection<EnergyPrice>> GetEnergyPrices()
    {
        var product = FactoryMethod();

        var prices = await product.GetEnergyPricesAsync();
        return prices;
    }
}
