using EnergyPriceChecker.Data;
using EnergyPriceChecker.Jobs.Abstract;
using EnergyPriceChecker.Jobs.Implementation;
using EnergyPriceChecker.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;

namespace EnergyPriceChecker;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("SqlLiteConnection");
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(connectionString)
                );

                services.AddHttpClient<EnergyPriceApiClient>(client =>
                {
                    client.BaseAddress = new Uri("https://api.energyzero.nl/v1/");
                });

                services.AddQuartz(q =>
                {
                    q.SchedulerName = "Energy price Scheduler";

                    q.UseMicrosoftDependencyInjectionScopedJobFactory();

                    var jobKey = new JobKey("Update new energy prices");
                    q.AddJob<EnergyPricesJob>(opts => opts.WithIdentity(jobKey));
                    q.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithInterval(TimeSpan.FromHours(24))
                            .RepeatForever())
                    );
                });
                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            });
}
