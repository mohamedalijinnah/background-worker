using EnergyPriceChecker.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPriceChecker.Jobs.Abstract;

public interface IEnergyJob
{
    Task<IReadOnlyCollection<EnergyPrice>> GetEnergyPricesAsync();
}
