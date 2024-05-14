using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Repositories
{
    public interface IRebateRepository
    {
        Rebate GetRebate(string rebateIdentifier);
        Rebate StoreCalculation(Rebate account, decimal rebateAmount);
    }
}
