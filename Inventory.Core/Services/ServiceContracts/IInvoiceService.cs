using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface IInvoiceService
    {
        void Save(Invoice invoice);
        long GetTotalNumberOfInvoiceRecordsInDatabase();
        IEnumerable<Invoice> LoadInvoicesFromDatabase(long skip, int count);
        Task<long> GetTotalNumberOfInvoiceRecordsInDatabase(string code = null);
        Task<IEnumerable<Invoice>> LoadInvoicesFromDatabase(long skip, int count, string code = null);
    }
}
