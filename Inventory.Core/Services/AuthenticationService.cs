using Inventory.Core.Exceptions;
using Inventory.Core.Models;
using Inventory.EntityFramework.DataModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly IPasswordHasher _PasswordHasher;

        public async Task<Employee> Login(string username, string password)
        {
            Employee loadedEmployee = await _EmployeeService.LoadEmployee(username);

            if (loadedEmployee == null)
            {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult = _PasswordHasher.VerifyHashedPassword(loadedEmployee.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return loadedEmployee;
        }

        public async Task<RegistrationResult> Register(Employee employee, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            Employee foundEmployee = await _EmployeeService.LoadEmployee(employee.Username);
            if (foundEmployee != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _PasswordHasher.HashPassword(password);

                employee.PasswordHash = hashedPassword;

                await _EmployeeService.Save(employee);
            }

            return result;
        }
    }
}
