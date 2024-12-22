using EnergyPriceChecker.Data;
using EnergyPriceChecker.Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnergyPriceChecker.Service;

public class EnergyPriceApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public EnergyPriceApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<EnergyPrice>> GetLatestEnergyPricesAsync(UsageType usageType)
    {
        var fromDate = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
        var toDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
        var includeBtw = true;
        var interval = 4;

        var urlParams = $"fromDate={fromDate}&tillDate={toDate}&interval={interval}&usageType={(int)usageType}&inclBtw={includeBtw}";

        var response = await _httpClient.GetFromJsonAsync<PricesResponseDto>($"energyprices?{urlParams}", _serializerOptions);

        List<EnergyPrice> energyPrices = new();
        energyPrices.AddRange(response.Prices.OrderBy((item) => item.ReadingDate)
            .Select((item) => item.ToEnergyPrice(usageType)));

        int priceRunnerIndex = 0;
        foreach (var pr in energyPrices)
        {
            if (pr.Price == 0)
            {
                pr.Price = energyPrices[priceRunnerIndex - 1].Price;
            }
            priceRunnerIndex++;
        }

        return energyPrices;
    }
}
