using Inventory.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _NavigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _NavigationStore = navigationStore;

            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public BaseViewModel? CurrentViewModel => _NavigationStore.CurrentViewModel;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
