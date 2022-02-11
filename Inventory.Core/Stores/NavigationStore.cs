using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class NavigationStore
    {
        private BaseViewModel? _CurrentViewModel;
        private Stack<BaseViewModel?> _ViewModelsStack = new Stack<BaseViewModel?>();

        public BaseViewModel? CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            if (CurrentViewModel != null)
                _ViewModelsStack.Push(_CurrentViewModel);

            CurrentViewModelChanged?.Invoke();
        }

        public void Close()
        {
            if (_ViewModelsStack.Count >= 2)
            {
                _ViewModelsStack.Pop();
                CurrentViewModel = _ViewModelsStack.Peek();
            }
            else if (_ViewModelsStack.Count == 1)
            {
                _ViewModelsStack.Pop();
                CurrentViewModel = null;
            }
            else
            {
                CurrentViewModel = null;
            }
        }
    }
}
