using Inventory.Core.Services;
using Inventory.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly AccountStore _AccountStore;

        public NavigationBarViewModel(INavigationService navigationService, AccountStore accountStore)
        {
            _NavigationService = navigationService;
            _AccountStore = accountStore;

            _AccountStore.CurrentAccountChanged += AccountStore_CurrentAccountChanged;

            GoBackCommand = new RelayCommand(GoBack);
            GoHomeCommand = new RelayCommand(GoHome);
            LogoutCommand = new RelayCommand(Logout);
        }

        public string LoggedInUserFullName { get => _AccountStore?.CurrentAccount?.FullName; }

        public ICommand GoBackCommand { get; }
        public ICommand GoHomeCommand { get; }
        public ICommand LogoutCommand { get; }

        private void GoBack()
        {
            _NavigationService.Close();
        }

        private void GoHome()
        {
            _NavigationService.NavigateHome();
        }

        private void Logout()
        {
            _AccountStore.Logout();
            _NavigationService.Logout();
        }

        private void AccountStore_CurrentAccountChanged()
        {
            OnPropertyChanged(LoggedInUserFullName);
        }
    }
}
