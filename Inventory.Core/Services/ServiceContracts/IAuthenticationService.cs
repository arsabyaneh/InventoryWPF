using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Employee> Login(string username, string password);
        Task<RegistrationResult> Register(Employee employee, string password, string confirmPassword);
    }

    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        UsernameAlreadyExists
    }
}
