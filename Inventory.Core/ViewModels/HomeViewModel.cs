using Inventory.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            _NavigationService = navigationService;
        }
    }
}
