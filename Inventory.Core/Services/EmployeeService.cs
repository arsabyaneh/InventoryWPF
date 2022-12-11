using Inventory.EntityFramework;
using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Func<IUnitOfWork> _CreateUnitOfWork;

        public EmployeeService(Func<IUnitOfWork> createUnitOfWork)
        {
            _CreateUnitOfWork = createUnitOfWork ?? throw new ArgumentNullException(nameof(createUnitOfWork));
        }

        public IEnumerable<Role> LoadAllRoles()
        {
            using (IUnitOfWork uow = _CreateUnitOfWork())
            {
                return uow.RoleRepository.Get(orderBy: o => o.OrderBy(x => x.Title));
            }
        }

        public async Task Save(Employee employee)
        {
            using (IUnitOfWork uow = _CreateUnitOfWork())
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
                using (IUnitOfWork uow = _CreateUnitOfWork())
                {
                    uow.RoleRepository.Update(role);
                    uow.Save();
                }
            }
        }

        public async Task<Employee?> LoadEmployee(string username)
        {
            using (IUnitOfWork uow = _CreateUnitOfWork())
            {
                return await uow.EmployeeRepository.GetDbSet().FirstOrDefaultAsync(x => x.Username == username);
            }
        }
    }
}
