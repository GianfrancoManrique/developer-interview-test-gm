using Microsoft.Extensions.DependencyInjection;
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

        var service = serviceProvider.GetService<IRebateService>();
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r1",
            ProductIdentifier = "p1",
            Volume = 5
        };

        var calculateResult = service.Calculate(request);
        string message = "Rebate calculation fails";

        if (calculateResult.Success)
        {
            var storeResult = service.StoreCalculation(calculateResult);
            message = $"Rebate calculation / saving was successfull: {storeResult?.Amount}";
        }

        Console.WriteLine(message);
    }
}
