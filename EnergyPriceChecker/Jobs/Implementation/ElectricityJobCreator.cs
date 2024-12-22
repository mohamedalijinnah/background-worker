using EnergyPriceChecker.Jobs.Abstract;
using EnergyPriceChecker.Service;

namespace EnergyPriceChecker.Jobs.Implementation;

public class ElectricityJobCreator : EnergyJobCreator
{
    private readonly EnergyPriceApiClient _energyPriceApiClient;
    public ElectricityJobCreator(EnergyPriceApiClient energyPriceApiClient)
    {
        _energyPriceApiClient = energyPriceApiClient;
    }
    public override IEnergyJob FactoryMethod()
    {
        return new EletricityJob(_energyPriceApiClient);
    }
}
