using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.MockData
{
    public static class RebateMockData
    {
        public static List<Rebate> Rebates = new List<Rebate>()
        {
            new Rebate() { Identifier = "r1", Incentive = IncentiveType.AmountPerUom, Amount = 100, Percentage = 0.10m },
            new Rebate() { Identifier = "r2", Incentive = IncentiveType.FixedCashAmount, Amount = 200, Percentage = 0.20m },
            new Rebate() { Identifier = "r3", Incentive = IncentiveType.FixedRateRebate, Amount = 300, Percentage = 0.30m }
        };

        public static Rebate updatedRebate = new Rebate()
        {
            Identifier = "r1",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 500,
            Percentage = 0.10m
        };

        public static Rebate invalidRebate = new Rebate()
        {
            Identifier = "r4",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0,
            Percentage = 0.30m
        };
    }
}
