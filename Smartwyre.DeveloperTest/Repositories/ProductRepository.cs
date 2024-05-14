using Smartwyre.DeveloperTest.MockData;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProduct(string productIdentifier)
        {
            var result = ProductMockData.Products.FirstOrDefault(P => P.Identifier == productIdentifier);
            return result;
        }
    }
}
