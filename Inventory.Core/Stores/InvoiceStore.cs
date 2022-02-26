using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class InvoiceStore
    {
        public event Action<InvoiceViewModel> InvoiceDeleted;
        public event Action<InvoiceViewModel> InvoiceUpdated;

        public void DeleteInvoice(InvoiceViewModel invoiceViewModel)
        {
            InvoiceDeleted?.Invoke(invoiceViewModel);
        }

        public void UpdateInvoice(InvoiceViewModel invoiceViewModel)
        {
            InvoiceUpdated?.Invoke(invoiceViewModel);
        }
    }
}
