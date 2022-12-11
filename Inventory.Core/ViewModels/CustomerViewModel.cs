using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly IModalNavigationService _ModalNavigationService;
        private readonly ICustomerService _CustomerService;

        private string? _Code;
        private string? _FirstName;
        private string? _LastName;
        private int _Gender;
        private DateTime? _BirthDate;
        private string? _Email;
        private string? _Telephone;
        private string? _Address;

        private Customer _Customer;

        public CustomerViewModel(IModalNavigationService modalNavigationService, ICustomerService customerService)
        {
            _ModalNavigationService = modalNavigationService;
            _CustomerService = customerService;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string? Code { get => _Code; set => SetProperty(ref _Code, value); }
        public string? FirstName { get => _FirstName; set => SetProperty(ref _FirstName, value); }
        public string? LastName { get => _LastName; set => SetProperty(ref _LastName, value); }
        public int Gender { get => _Gender; set => SetProperty(ref _Gender, value); }
        public DateTime? BirthDate { get => _BirthDate; set => SetProperty(ref _BirthDate, value); }
        public string? Email { get => _Email; set => SetProperty(ref _Email, value); }
        public string? Telephone { get => _Telephone; set => SetProperty(ref _Telephone, value); }
        public string? Address { get => _Address; set => SetProperty(ref _Address, value); }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public Customer Customer
        {
            get
            {
                _Customer = new Customer
                {
                    Id = _Customer != null ? _Customer.Id : 0,
                    Code = Code ?? string.Empty,
                    FirstName = FirstName ?? string.Empty,
                    LastName = LastName ?? string.Empty,
                    Gender = Gender == 0 ? false : true,
                    BirthDate = BirthDate != null ? BirthDate.Value : DateTime.UtcNow,
                    Email = Email ?? string.Empty,
                    Telephone = Telephone ?? string.Empty,
                    Address = Address
                };

                return _Customer;
            }

            set
            {
                _Customer = value;

                Code = _Customer?.Code;
                FirstName = _Customer?.FirstName;
                LastName = _Customer?.LastName;
                Gender = _Customer?.Gender == false ? 0 : 1;
                BirthDate = _Customer?.BirthDate;
                Email = _Customer?.Email;
                Telephone = _Customer?.Telephone;
                Address = _Customer?.Address;
            }
        }

        private void Ok()
        {
            _CustomerService.Save(Customer);
            _ModalNavigationService.Close();
        }

        private void Cancel()
        {
            _ModalNavigationService.Close();
        }
    }
}
