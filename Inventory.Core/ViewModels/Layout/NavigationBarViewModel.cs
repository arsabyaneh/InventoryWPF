using Inventory.Core.Services;
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

        public NavigationBarViewModel(INavigationService navigationService)
        {
            _NavigationService = navigationService;
        }

        public ICommand GoBackCommand { get; }
        public ICommand GoHomeCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}
