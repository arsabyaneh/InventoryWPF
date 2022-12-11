using Inventory.Core.ViewModels;
using Inventory.Core.ViewModels.Base;

namespace Inventory.Core.Factories
{
    public interface IViewModelFactory
    {
        BaseViewModel? CreateViewModel(ViewType viewType);
    }
}