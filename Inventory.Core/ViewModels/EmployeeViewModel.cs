using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly IModalNavigationService _ModalNavigationService;
        private readonly IAuthenticationService _AuthenticationService;
        private readonly IEmployeeService _EmployeeService;

        private string? _Code;
        private string? _FirstName;
        private string? _LastName;
        private int _Gender;
        private DateTime? _BirthDate;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private string? _Email;
        private string? _Telephone;
        private string? _Address;
        private string? _Username;
        private string _Password;
        private string _ConfirmPassword;
        private Role? _Role;

        private Employee _Employee;

        public EmployeeViewModel(IModalNavigationService modalNavigationService,
            IAuthenticationService authenticationService, IEmployeeService employeeService)
        {
            _ModalNavigationService = modalNavigationService;
            _EmployeeService = employeeService;
            _AuthenticationService = authenticationService;

            Roles = _EmployeeService.LoadAllRoles().ToList();

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string? Code { get => _Code; set => SetProperty(ref _Code, value); }
        public string? FirstName { get => _FirstName; set => SetProperty(ref _FirstName, value); }
        public string? LastName { get => _LastName; set => SetProperty(ref _LastName, value); }
        public int Gender { get => _Gender; set => SetProperty(ref _Gender, value); }
        public DateTime? BirthDate { get => _BirthDate; set => SetProperty(ref _BirthDate, value); }
        public DateTime? StartDate { get => _StartDate; set => SetProperty(ref _StartDate, value); }
        public DateTime? EndDate { get => _EndDate; set => SetProperty(ref _EndDate, value); }
        public string? Email { get => _Email; set => SetProperty(ref _Email, value); }
        public string? Telephone { get => _Telephone; set => SetProperty(ref _Telephone, value); }
        public string? Address { get => _Address; set => SetProperty(ref _Address, value); }
        public string? Username { get => _Username; set => SetProperty(ref _Username, value); }
        public string Password { get => _Password; set => SetProperty(ref _Password, value); }
        public string ConfirmPassword { get => _ConfirmPassword; set => SetProperty(ref _ConfirmPassword, value); }
        public Role? Role { get => _Role; set => SetProperty(ref _Role, value); }

        public List<Role> Roles { get; set; }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public Employee Employee
        {
            get
            {
                _Employee = new Employee
                {
                    Id = _Employee != null ? _Employee.Id : 0,
                    Code = Code ?? string.Empty,
                    FirstName = FirstName ?? string.Empty,
                    LastName = LastName ?? string.Empty,
                    Gender = Gender == 0 ? false : true,
                    BirthDate = BirthDate != null ? BirthDate.Value : DateTime.UtcNow,
                    StartDate = StartDate != null ? StartDate.Value : DateTime.UtcNow,
                    EndDate = EndDate,
                    Email = Email ?? string.Empty,
                    Telephone = Telephone ?? string.Empty,
                    Address = Address ?? string.Empty,
                    IsActive = EndDate == null ? true : false,
                    RoleId = Role != null ? Role.Id : 0,
                    Username = Username ?? string.Empty
                };

                return _Employee;
            }

            set
            {
                _Employee = value;

                Code = _Employee?.Code;
                FirstName = _Employee?.FirstName;
                LastName = _Employee?.LastName;
                Gender = _Employee?.Gender == false ? 0 : 1;
                BirthDate = _Employee?.BirthDate;
                StartDate = _Employee?.StartDate;
                EndDate = _Employee?.EndDate;
                Email = _Employee?.Email;
                Telephone = _Employee?.Telephone;
                Address = _Employee?.Address;
                Username = _Employee?.Username;
                Role = Roles.FirstOrDefault(x => x.Title == _Employee?.Role?.Title);
            }
        }

        private void Ok()
        {
            _AuthenticationService.Register(Employee, Password, ConfirmPassword);
            _ModalNavigationService.Close();
        }

        private void Cancel()
        {
            _ModalNavigationService.Close();
        }
    }
}
