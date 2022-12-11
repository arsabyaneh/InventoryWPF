using Inventory.Core.Stores;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels.Base
{
    public interface IViewModel
    {
        Task<BaseViewModel> Initialise(IStore store, EntityState entityState, object? entity = null);
    }
}
