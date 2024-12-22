using EnergyPriceChecker.Jobs.Abstract;
using EnergyPriceChecker.Service;

namespace EnergyPriceChecker.Jobs.Implementation;

public class GasJobCreator : EnergyJobCreator
{
    private readonly EnergyPriceApiClient _energyPriceApiClient;
    public GasJobCreator(EnergyPriceApiClient energyPriceApiClient)
    {
        _energyPriceApiClient = energyPriceApiClient;
    }
    public override IEnergyJob FactoryMethod()
    {
        return new GasJob(_energyPriceApiClient);
    }
}