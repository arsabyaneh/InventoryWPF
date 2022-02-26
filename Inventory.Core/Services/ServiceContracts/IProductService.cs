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
        void Delete(long productId);
        IEnumerable<Price> LoadPrices(long productId);
        Product LoadProduct(string code);
        decimal LoadProductSellPrice(string code);
        long GetTotalNumberOfProductRecordsInDatabase();
        IEnumerable<Product> LoadProductsFromDatabase(long skip, int count);
        Task<long> GetTotalNumberOfProductRecordsInDatabase(string title = null);
        Task<IEnumerable<Product>> LoadProductsFromDatabase(long skip, int count, string title = null);
    }
}
