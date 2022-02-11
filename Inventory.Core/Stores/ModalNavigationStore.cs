using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class ModalNavigationStore
    {
        private BaseViewModel? _CurrentViewModel;

        public BaseViewModel? CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public bool IsOpen => CurrentViewModel != null;
        public double ControlWidth => CurrentViewModel != null ? CurrentViewModel.ControlWidth : 500.0;

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public void Close()
        {
            CurrentViewModel = null;
        }
    }
}
