using Smartwyre.DeveloperTest.MockData;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Repositories
{
    public class RebateRepository : IRebateRepository
    {
        public Rebate GetRebate(string rebateIdentifier)
        {
            var result = RebateMockData.Rebates.FirstOrDefault(P => P.Identifier == rebateIdentifier);
            return result;
        }

        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
            account.Amount = rebateAmount;
            List<Rebate> rebates = RebateMockData.Rebates;
            rebates.Add(account);
            Console.WriteLine(account.Amount.ToString());
        }
    }
}
