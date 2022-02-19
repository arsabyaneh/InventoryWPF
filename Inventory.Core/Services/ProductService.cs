using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class ProductService : IProductService
    {
        public void Save(Product product)
        {
            if (product == null)
                return;

            IEnumerable<Price> prices = LoadPrices(product.Id);

            using (UnitOfWork uow = new UnitOfWork())
            {
                Brand brand = uow.InsertIfNotExists(uow.BrandRepository, product.Brand, x => x.Title == product.Brand.Title && x.Address == product.Brand.Address);

                if (brand != null)
                {
                    product.BrandId = brand.Id;
                    product.Brand = null;
                }

                uow.ProductRepository.Update(product);

                foreach (var price in product.Prices)
                {
                    if (price.EntityState == EntityState.Deleted)
                        uow.SetEntryState(price, EntityState.Deleted);
                }

                uow.Save();
            }
        }

        public IEnumerable<Price> LoadPrices(long productId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.PriceRepository.Get(filter: x => x.ProductId == productId, orderBy: o => o.OrderByDescending(x => x.PriceDate));
            }
        }

        public Product LoadProduct(string code)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.ProductRepository.GetDbSet().Include(x => x.Prices).Where(x => x.Code == code).FirstOrDefault();
            }
        }

        public decimal LoadProductSellPrice(string code)
        {
            Product product = LoadProduct(code);
            return product.Prices.OrderByDescending(x => x.PriceDate).First().Sell;
        }

        public long GetTotalNumberOfProductRecordsInDatabase()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.ProductRepository.GetDbSet().LongCount();
            }
        }

        public IEnumerable<Product> LoadProductsFromDatabase(long skip, int count)
        {
            IEnumerable<Product> products;

            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = uow.ProductRepository.GetDbSet().AsQueryable();

                while (skip > int.MaxValue)
                {
                    query = query.Skip(int.MaxValue);
                    skip = skip - int.MaxValue;
                }
                int skipInt = (int)skip;

                products = query.OrderBy(x => x.Code)
                    .Include(x => x.Brand)
                    .Include(x => x.Prices)
                    .Skip(skipInt).Take(count).ToList();
            }

            return products;
        }

        public async Task<long> GetTotalNumberOfProductRecordsInDatabase(string title = null)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if (!string.IsNullOrWhiteSpace(title))
                    return uow.ProductRepository.GetDbSet().Where(x => x.Title == title).LongCount();
                else
                    return uow.ProductRepository.GetDbSet().LongCount();
            }
        }

        public async Task<IEnumerable<Product>> LoadProductsFromDatabase(long skip, int count, string title = null)
        {
            IEnumerable<Product> products;

            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = uow.ProductRepository.GetDbSet().AsQueryable();

                while (skip > int.MaxValue)
                {
                    query = query.Skip(int.MaxValue);
                    skip = skip - int.MaxValue;
                }
                int skipInt = (int)skip;

                if (!string.IsNullOrWhiteSpace(title))
                    products = await query.OrderBy(x => x.Code)
                    .Include(x => x.Brand)
                    .Include(x => x.Prices)
                    .Where(x => x.Title == title)
                    .Skip(skipInt).Take(count).ToListAsync();
                else
                    products = await query.OrderBy(x => x.Code)
                    .Include(x => x.Brand)
                    .Include(x => x.Prices)
                    .Skip(skipInt).Take(count).ToListAsync();
            }

            return products;
        }
    }
}
