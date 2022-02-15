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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee Employee { get; set; }
    }
}
