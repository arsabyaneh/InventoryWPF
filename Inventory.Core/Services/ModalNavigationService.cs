using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
using System;

namespace Inventory.Core.Services
{
    public class ModalNavigationService : IModalNavigationService
    {
        private readonly ModalNavigationStore _ModalNavigationStore;

        public ModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            _ModalNavigationStore = modalNavigationStore;
        }

        public void Navigate<TViewModel>(Func<TViewModel> createViewModel) where TViewModel : BaseViewModel
        {
            _ModalNavigationStore.CurrentViewModel = createViewModel();
        }

        public void Close()
        {
            _ModalNavigationStore?.Close();
        }

        public void NavigateHome()
        {
            
        }

        public void Logout()
        {
            
        }
    }
}
