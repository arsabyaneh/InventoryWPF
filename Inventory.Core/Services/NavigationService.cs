using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationStore _NavigationStore;
        private readonly Func<NavigationService, NavigationBarViewModel> _CreateNavigationBarViewModel;

        public NavigationService(NavigationStore navigationStore, Func<NavigationService, NavigationBarViewModel> createNavigationBarViewModel)
        {
            _NavigationStore = navigationStore;
            _CreateNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate<TViewModel>(Func<TViewModel> createViewModel) where TViewModel : BaseViewModel
        {
            _NavigationStore.CurrentViewModel = new LayoutViewModel(_CreateNavigationBarViewModel(this), createViewModel());
        }

        public void Close()
        {
            _NavigationStore?.Close();
        }
    }
}
