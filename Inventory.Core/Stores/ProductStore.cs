using Inventory.Core.ViewModels;
using System;

namespace Inventory.Core.Stores
{
    public class ProductStore : IStore
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
