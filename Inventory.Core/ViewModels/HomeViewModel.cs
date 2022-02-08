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
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;

        public HomeViewModel(INavigationService navigationService, INavigationService modalNavigationService)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;

            AddCommand = new RelayCommand(Add);
        }

        public ICommand AddCommand { get; }

        private void Add()
        {
            _ModalNavigationService.Navigate(() => new AddNewItemViewModel());
        }
    }
}
