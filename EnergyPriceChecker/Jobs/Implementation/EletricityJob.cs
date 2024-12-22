using EnergyPriceChecker.Data;
using EnergyPriceChecker.Jobs.Abstract;
using EnergyPriceChecker.Service;
using EnergyPriceChecker.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPriceChecker.Jobs.Implementation;

public class EletricityJob : IEnergyJob
{
    private readonly EnergyPriceApiClient _httpClient;

    public EletricityJob(EnergyPriceApiClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IReadOnlyCollection<EnergyPrice>> GetEnergyPricesAsync()
    {
        var latestPrices = await _httpClient.GetLatestEnergyPricesAsync(UsageType.Electricity);
        return latestPrices;
    }
}
