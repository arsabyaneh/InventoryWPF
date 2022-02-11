using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
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

        public void Save(Employee employee)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.EmployeeRepository.Update(employee);
                uow.Save();
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
    }
}
