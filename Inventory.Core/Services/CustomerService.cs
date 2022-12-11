using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
using System;
using System.Linq;

namespace Inventory.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Func<IUnitOfWork> _CreateUnitOfWork;

        private Customer? _CashDeskCustomer;

        public CustomerService(Func<IUnitOfWork> createUnitOfWork)
        {
            _CreateUnitOfWork = createUnitOfWork ?? throw new ArgumentNullException(nameof(createUnitOfWork));
        }

        public Customer? CashDeskCustomer 
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
            using (IUnitOfWork uow = _CreateUnitOfWork())
            {
                uow.CustomerRepository.Update(customer);
                uow.Save();
            }
        }

        public Customer? LoadCashDeskCustomer(string code)
        {
            using (IUnitOfWork uow = _CreateUnitOfWork())
            {
                return uow.CustomerRepository.Get(filter: x => x.Code == code).FirstOrDefault();
            }
        }
    }
}
