using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Models
{
    public class Account
    {
        public string FirstName { get => Employee?.FirstName; }
        public string LastName { get => Employee?.LastName; }
        public string FullName { get => $"{LastName}, {FirstName}"; }

        public Employee Employee { get; set; }
    }
}
