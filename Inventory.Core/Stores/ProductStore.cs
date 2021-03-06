using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class ProductStore
    {
        public event Action<ProductViewModel> ProductDeleted;
        public event Action<ProductViewModel> ProductUpdated;

        public void DeleteProduct(ProductViewModel productViewModel)
        {
            ProductDeleted?.Invoke(productViewModel);
        }

        public void UpdateProduct(ProductViewModel productViewModel)
        {
            ProductUpdated?.Invoke(productViewModel);
        }
    }
}
