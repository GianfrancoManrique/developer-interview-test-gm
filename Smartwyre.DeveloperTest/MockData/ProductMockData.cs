using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.MockData
{
    public static class ProductMockData
    {
        public static List<Product> Products = new List<Product>()
        {
            new Product() { Id = 1, Identifier = "p1", Price = 10, Uom = "uom1", SupportedIncentives = SupportedIncentiveType.AmountPerUom },
            new Product() { Id = 2, Identifier = "p2", Price = 20, Uom = "uom2", SupportedIncentives = SupportedIncentiveType.FixedCashAmount },
            new Product() { Id = 3, Identifier = "p3", Price = 20, Uom = "uom3", SupportedIncentives = SupportedIncentiveType.FixedRateRebate }
        };
    }
}
