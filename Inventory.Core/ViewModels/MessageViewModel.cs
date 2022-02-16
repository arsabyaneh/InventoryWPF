using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string _Message;
        public string Message
        {
            get => _Message;
            set
            {
                SetProperty(ref _Message, value);
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
