using Inventory.Core.ViewModels;
using System;

namespace Inventory.Core.Stores
{
    public class InvoiceStore : IStore
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
