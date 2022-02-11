using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Role> LoadAllRoles();
        void Save(Employee employee);
        void Save(Role role);
    }
}
