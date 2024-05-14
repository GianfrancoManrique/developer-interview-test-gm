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
            new Rebate() { Identifier = "r1", Incentive = IncentiveType.AmountPerUom, Amount = 100, Percentage = 10 },
            new Rebate() { Identifier = "r2", Incentive = IncentiveType.FixedCashAmount, Amount = 200, Percentage = 20 },
            new Rebate() { Identifier = "r3", Incentive = IncentiveType.FixedRateRebate, Amount = 300, Percentage = 30 }
        };
    }
}
