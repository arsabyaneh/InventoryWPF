using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
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
                uow.Save();
            }
        }
    }
}
