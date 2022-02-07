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
        private readonly ModalNavigationStore _ModalNavigationStore;

        public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _NavigationStore = navigationStore;
            _ModalNavigationStore = modalNavigationStore;

            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _ModalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        public BaseViewModel? CurrentViewModel => _NavigationStore.CurrentViewModel;
        public BaseViewModel? CurrentModalViewModel => _ModalNavigationStore.CurrentViewModel;
        public bool IsOpen => _ModalNavigationStore.IsOpen;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }
    }
}
