using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface IProductService
    {
        void Save(Product product);
        IEnumerable<Price> LoadPrices(long productId);
        Product LoadProduct(string code);
        decimal LoadProductSellPrice(string code);
    }
}
