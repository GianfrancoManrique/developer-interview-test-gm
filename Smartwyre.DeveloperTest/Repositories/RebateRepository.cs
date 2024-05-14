using Smartwyre.DeveloperTest.MockData;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Smartwyre.DeveloperTest.Repositories
{
    public class RebateRepository : IRebateRepository
    {
        public Rebate GetRebate(string rebateIdentifier)
        {
            var result = RebateMockData.Rebates.FirstOrDefault(P => P.Identifier == rebateIdentifier);
            return result;
        }

        public Rebate StoreCalculation(Rebate account, decimal rebateAmount)
        {
            var rebate = RebateMockData.Rebates.FirstOrDefault(p => p.Identifier == account.Identifier);
            rebate.Amount = rebateAmount;

            return rebate;
        }
    }
}
