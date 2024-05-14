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

        bool continueExecuting = true;

        while (continueExecuting)
        {
            string message = "Invalid inputs";

            Console.Clear();

            Console.WriteLine("Enter your rebate identifier:");
            string rebateIdentifier = Console.ReadLine();

            Console.WriteLine("Enter your product identifier:");
            string productIdentifier = Console.ReadLine();

            Console.WriteLine("Enter your volume:");
            string _volume = Console.ReadLine();
            decimal volume = 0;

            bool validInputs = !string.IsNullOrEmpty(rebateIdentifier) && !string.IsNullOrEmpty(productIdentifier) && Decimal.TryParse(_volume, out volume);
            if (validInputs)
            {
                CalculateRebateRequest request = new CalculateRebateRequest
                {
                    RebateIdentifier = rebateIdentifier,
                    ProductIdentifier = productIdentifier,
                    Volume = volume
                };

                var calculateResult = service.Calculate(request);
                message = calculateResult.Message;

                if (calculateResult.Success)
                {
                    var storeResult = service.StoreCalculation(calculateResult);
                    message = $"Rebate calculation / saving was successfull: {storeResult?.Amount}";
                }
            }

            Console.WriteLine($"Message: {message}");

            Console.WriteLine("Do you want to calculate another rebate? (y/n): ");

            string response = Console.ReadLine().Trim().ToLower();

            if (response != "y")
            {
                continueExecuting = false;
            }
        }
    }
}
