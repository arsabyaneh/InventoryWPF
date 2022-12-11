using Inventory.Core.Exceptions;
using Inventory.Core.Models;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels.Base;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly IAuthenticationService _AuthenticationService;
        private readonly HomeViewModel _HomeViewModel;
        private readonly AccountStore _AccountStore;

        private string _Username;
        private string _Password;

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService, HomeViewModel homeViewModel, AccountStore accountStore)
        {
            _NavigationService = navigationService;
            _AuthenticationService = authenticationService;
            _HomeViewModel = homeViewModel;
            _AccountStore = accountStore;

            ViewModelType = ViewType.Login;
            ErrorMessageViewModel = new MessageViewModel();

            LoginCommand = new RelayAsyncCommand(Login, ex => throw ex);
        }

        public string Username { get => _Username; set => SetProperty(ref _Username, value); }
        public string Password { get => _Password; set => SetProperty(ref _Password, value); }
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }
        public MessageViewModel ErrorMessageViewModel { get; }
        public ICommand LoginCommand { get; }

        public async Task Login()
        {
            ErrorMessage = string.Empty;

            try
            {
                Employee employee = await _AuthenticationService.Login(Username, Password);
                
                _AccountStore.CurrentAccount = new Account
                {
                    Employee = employee
                };

                Username = string.Empty;
                Password = string.Empty;

                _NavigationService.Navigate(() => _HomeViewModel);
            }
            catch (UserNotFoundException)
            {
                ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                ErrorMessage = "Login failed.";
            }
        }
    }
}
