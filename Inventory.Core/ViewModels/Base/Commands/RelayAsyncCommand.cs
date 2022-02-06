using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class RelayAsyncCommand : ICommand
    {
        private readonly Func<Task> _CommandAction;
        private readonly Action<Exception> _OnException;

        public RelayAsyncCommand(Func<Task> commandAction, Action<Exception> onException)
        {
            _CommandAction = commandAction;
            _OnException = onException;
        }

        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                await _CommandAction();
            }
            catch (Exception ex)
            {
                _OnException?.Invoke(ex);
            }
        }
    }
}
