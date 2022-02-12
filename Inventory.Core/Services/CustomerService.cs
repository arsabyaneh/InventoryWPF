using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class CustomerService : ICustomerService
    {
        public void Save(Customer customer)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.CustomerRepository.Update(customer);
                uow.Save();
            }
        }
    }
}
