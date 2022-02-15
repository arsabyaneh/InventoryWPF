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
        Task Save(Employee employee);
        void Save(Role role);
        Task<Employee> LoadEmployee(string username);
    }
}
