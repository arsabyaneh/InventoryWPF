using Inventory.Core.Stores;
using Inventory.Core.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public double ControlWidth { get; set; } = double.NaN;
        public ViewType ViewModelType { get; set; } = ViewType.None;
        public EntityState EntityState { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string? name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = default)
        {
            if (object.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            OnPropertyChanged(propertyName);
            
            return true;
        }

        public virtual async Task<BaseViewModel> Initialise(IStore store, EntityState entityState, object? entity = null)
        {
            return await Task.FromResult(this);
        }
    }

    public enum ViewModelType
    {
        None,
        Login,
        Home
    }
}
