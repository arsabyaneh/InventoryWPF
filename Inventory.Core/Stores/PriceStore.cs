using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class PriceStore
    {
        public event Action<PriceViewModel> PriceDeleted;

        public void DeletePrice(PriceViewModel priceViewModel)
        {
            PriceDeleted?.Invoke(priceViewModel);
        }
    }
}
