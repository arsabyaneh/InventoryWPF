using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface ICustomerService
    {
        Customer? CashDeskCustomer { get; }

        void Save(Customer customer);
    }
}
