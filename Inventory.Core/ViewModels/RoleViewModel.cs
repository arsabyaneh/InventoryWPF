using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class RoleViewModel : BaseViewModel
    {
        private readonly IModalNavigationService _ModalNavigationService;
        private readonly IEmployeeService _EmployeeService;

        private string _Title;

        public RoleViewModel(IModalNavigationService modalNavigationService, IEmployeeService employeeService)
        {
            _ModalNavigationService = modalNavigationService;
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
            _ModalNavigationService.Close();
        }

        private void Cancel()
        {
            _ModalNavigationService.Close();
        }
    }
}
