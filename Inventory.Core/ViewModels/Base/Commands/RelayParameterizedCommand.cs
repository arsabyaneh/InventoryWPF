using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class RelayParameterizedCommand : ICommand
    {
        private readonly Action<object> _CommandAction;

        public RelayParameterizedCommand(Action<object> action)
        {
            _CommandAction = action;
        }

        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _CommandAction(parameter);
        }
    }
}
