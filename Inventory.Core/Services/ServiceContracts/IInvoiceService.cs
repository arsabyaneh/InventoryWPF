using Inventory.EntityFramework.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface IInvoiceService
    {
        void Save(Invoice? invoice);
        Task Delete(long invoiceId);
        long GetTotalNumberOfInvoiceRecordsInDatabase();
        IEnumerable<Invoice> LoadInvoicesFromDatabase(long skip, int count);
        Task<long> GetTotalNumberOfInvoiceRecordsInDatabase(string code = null);
        Task<IEnumerable<Invoice>> LoadInvoicesFromDatabase(long skip, int count, string code = null);
        Task<IEnumerable<InvoiceItem>> LoadInvoiceItems(long invoiceId);
    }
}
