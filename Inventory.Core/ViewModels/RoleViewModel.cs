using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class RoleViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly IEmployeeService _EmployeeService;

        private string _Title;

        public RoleViewModel(INavigationService navigationService, IEmployeeService employeeService)
        {
            _NavigationService = navigationService;
            _EmployeeService = employeeService;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string Title { get => _Title; set => SetProperty(ref _Title, value); }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        private void Ok()
        {
            _EmployeeService.Save(new Role
            {
                Title = Title
            });
            _NavigationService.Close();
        }

        private void Cancel()
        {
            _NavigationService.Close();
        }
    }
}
