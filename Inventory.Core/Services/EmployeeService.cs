using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Role> LoadAllRoles()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.RoleRepository.Get(orderBy: o => o.OrderBy(x => x.Title));
            }
        }

        public async Task Save(Employee employee)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.EmployeeRepository.Update(employee);
                await uow.SaveAsync();
            }
        }

        public void Save(Role role)
        {
            IEnumerable<Role> roles = LoadAllRoles();

            if (roles.Any(x => x.Title == role.Title) == false)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.RoleRepository.Update(role);
                    uow.Save();
                }
            }
        }

        public async Task<Employee> LoadEmployee(string username)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return await uow.EmployeeRepository.GetDbSet().FirstOrDefaultAsync(x => x.Username == username);
            }
        }
    }
}
