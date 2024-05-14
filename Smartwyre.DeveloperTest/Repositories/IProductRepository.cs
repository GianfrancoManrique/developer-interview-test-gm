using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(string productIdentifier);
    }
}
