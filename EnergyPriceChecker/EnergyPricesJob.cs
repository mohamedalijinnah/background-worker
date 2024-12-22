using EnergyPriceChecker.Data;
using EnergyPriceChecker.Jobs.Implementation;
using EnergyPriceChecker.Service;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace EnergyPriceChecker;

[DisallowConcurrentExecution]
public class EnergyPricesJob : IJob
{
    private readonly ILogger<EnergyPricesJob> _logger;
    private readonly EnergyPriceApiClient _httpClient;
    private readonly AppDbContext _dbContext;

    public EnergyPricesJob(ILogger<EnergyPricesJob> logger, EnergyPriceApiClient httpClient, AppDbContext dbContext)
    {
        _logger = logger;
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation("Fetching latest Electricity prices");

            var electrictyJob = new ElectricityJobCreator(_httpClient);

            var latestElectricityPrices = await electrictyJob.GetEnergyPrices();

            _dbContext.AddRange(latestElectricityPrices);


            _logger.LogInformation("Fetching latest Gas prices");

            var gasJob = new GasJobCreator(_httpClient);

            var latestGasPrices = await gasJob.GetEnergyPrices();


            _dbContext.AddRange(latestGasPrices);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Latest prices saved into the database");
        }
        catch 
        {

            _logger.LogError("Failed to fetch energy prices");
            throw;
        }
       
    }
}
