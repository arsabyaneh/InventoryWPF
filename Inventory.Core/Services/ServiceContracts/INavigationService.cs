using Inventory.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface INavigationService
    {
        void Navigate<TViewModel>(Func<TViewModel> createViewModel) where TViewModel : BaseViewModel;
        void Close();
        void NavigateHome();
        public void Logout();
    }
}
