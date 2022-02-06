using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _CommandAction;

        public RelayCommand(Action commandAction)
        {
            _CommandAction = commandAction;
        }

        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _CommandAction();
        }
    }
}
