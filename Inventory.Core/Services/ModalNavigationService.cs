using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class ModalNavigationService : INavigationService
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
    }
}
