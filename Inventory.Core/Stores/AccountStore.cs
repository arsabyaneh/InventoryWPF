using Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Stores
{
    public class AccountStore
    {
        private Account _CurrentAccount;
        public Account CurrentAccount
        {
            get => _CurrentAccount;
            set
            {
                _CurrentAccount = value;
                CurrentAccountChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}
