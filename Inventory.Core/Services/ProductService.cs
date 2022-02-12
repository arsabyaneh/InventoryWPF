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
    }
}
