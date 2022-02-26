using Inventory.Core.Exceptions;
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
    public class InvoiceService : IInvoiceService
    {
        public void Save(Invoice invoice)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.InvoiceRepository.Update(invoice);

                foreach (var item in invoice.InvoiceItems)
                {
                    if (item.EntityState == EntityState.Deleted)
                        uow.SetEntryState(item, EntityState.Deleted);
                }

                uow.Save();
            }
        }

        public async Task Delete(long invoiceId)
        {
            IEnumerable<InvoiceItem> invoiceItems = await LoadInvoiceItems(invoiceId);

            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.InvoiceItemRepository.Delete(invoiceItems);
                    uow.InvoiceRepository.Delete(invoiceId);
                    await uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Could not delete the invoice!", ex);
            }
        }

        public long GetTotalNumberOfInvoiceRecordsInDatabase()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.InvoiceRepository.GetDbSet().LongCount();
            }
        }

        public IEnumerable<Invoice> LoadInvoicesFromDatabase(long skip, int count)
        {
            IEnumerable<Invoice> invoices;

            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = uow.InvoiceRepository.GetDbSet().AsQueryable();

                while (skip > int.MaxValue)
                {
                    query = query.Skip(int.MaxValue);
                    skip = skip - int.MaxValue;
                }
                int skipInt = (int)skip;

                invoices = query.OrderBy(x => x.Code)
                    .Include(x => x.Customer)
                    .Include(x => x.Employee)
                    .Skip(skipInt).Take(count).ToList();
            }

            return invoices;
        }

        public async Task<long> GetTotalNumberOfInvoiceRecordsInDatabase(string code = null)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if (!string.IsNullOrWhiteSpace(code))
                    return uow.InvoiceRepository.GetDbSet().Where(x => x.Code == code).LongCount();
                else
                    return uow.InvoiceRepository.GetDbSet().LongCount();
            }
        }

        public async Task<IEnumerable<Invoice>> LoadInvoicesFromDatabase(long skip, int count, string code = null)
        {
            IEnumerable<Invoice> invoices;

            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = uow.InvoiceRepository.GetDbSet().AsQueryable();

                while (skip > int.MaxValue)
                {
                    query = query.Skip(int.MaxValue);
                    skip = skip - int.MaxValue;
                }
                int skipInt = (int)skip;

                if (!string.IsNullOrWhiteSpace(code))
                    invoices = await query.OrderBy(x => x.Code)
                    .Include(x => x.Customer)
                    .Include(x => x.Employee)
                    .Where(x => x.Code == code)
                    .Skip(skipInt).Take(count).ToListAsync();
                else
                    invoices = await query.OrderBy(x => x.Code)
                    .Include(x => x.Customer)
                    .Include(x => x.Employee)
                    .Skip(skipInt).Take(count).ToListAsync();
            }

            return invoices;
        }

        public async Task<IEnumerable<InvoiceItem>> LoadInvoiceItems(long invoiceId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.InvoiceItemRepository.GetDbSet()
                    .Include(x => x.Product).Where(x => x.InvoiceId == invoiceId)
                    .OrderBy(x => x.Id).ToList();
            }
        }
    }
}
