using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Smartwyre.DeveloperTest.Repositories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
          .AddLogging()
          .AddSingleton<IRebateService, RebateService>()
          .AddSingleton<IProductRepository, ProductRepository>()
          .AddSingleton<IRebateRepository, RebateRepository>()
          .BuildServiceProvider();

        //serviceProvider
        //  .GetService<ILoggerFactory>()
        //  .AddConsole(LogLevel.Debug);

        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
        logger.LogDebug("Starting application");

        var service = serviceProvider.GetService<IRebateService>();
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r1",
            ProductIdentifier = "p1",
            Volume = 5
        };

        var result = service.Calculate(request);

        logger.LogDebug(result?.Success.ToString());
        Console.WriteLine(result?.Success.ToString());
    }
}
