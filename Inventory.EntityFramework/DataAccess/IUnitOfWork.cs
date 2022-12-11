using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.EntityFramework
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Brand> BrandRepository { get; }
        Repository<Customer> CustomerRepository { get; }
        Repository<Employee> EmployeeRepository { get; }
        Repository<Invoice> InvoiceRepository { get; }
        Repository<InvoiceItem> InvoiceItemRepository { get; }
        Repository<Price> PriceRepository { get; }
        Repository<Product> ProductRepository { get; }
        Repository<Role> RoleRepository { get; }

        void Save();
        Task SaveAsync();
        TEntity InsertIfNotExists<TEntity>(IRepository<TEntity> repository, TEntity entity, Expression<Func<TEntity, bool>> filter) where TEntity : class;
        void SetEntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class;
    }
}
