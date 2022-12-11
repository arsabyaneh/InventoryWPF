using Inventory.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class LayoutViewModel : BaseViewModel
    {
        public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, BaseViewModel contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
            IsNavigationBarVisible = contentViewModel.ViewModelType != ViewType.Login;
        }

        public NavigationBarViewModel NavigationBarViewModel { get; }
        public BaseViewModel ContentViewModel { get; }
        public bool IsNavigationBarVisible { get; }
    }
}
