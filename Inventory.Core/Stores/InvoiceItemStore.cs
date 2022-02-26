using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class InvoiceItemStore
    {
        public event Action<InvoiceItemViewModel> InvoiceItemDeleted;

        public void DeleteInvoiceItem(InvoiceItemViewModel invoiceItemViewModel)
        {
            InvoiceItemDeleted?.Invoke(invoiceItemViewModel);
        }
    }
}
