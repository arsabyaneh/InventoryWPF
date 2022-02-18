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
        private Customer _CashDeskCustomer;
        public Customer CashDeskCustomer 
        { 
            get
            {
                if (_CashDeskCustomer == null)
                    _CashDeskCustomer = LoadCashDeskCustomer("0000000000");

                return _CashDeskCustomer;
            }
        }

        public void Save(Customer customer)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.CustomerRepository.Update(customer);
                uow.Save();
            }
        }

        public Customer LoadCashDeskCustomer(string code)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.CustomerRepository.Get(filter: x => x.Code == code).FirstOrDefault();
            }
        }
    }
}
