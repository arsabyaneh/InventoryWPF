using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityFramework
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private InventoryDbContext _DbContext;
        private Repository<Brand> _BrandRepository;
        private Repository<Customer> _CustomerRepository;
        private Repository<Employee> _EmployeeRepository;
        private Repository<Invoice> _InvoiceRepository;
        private Repository<InvoiceItem> _InvoiceItemRepository;
        private Repository<Price> _PriceRepository;
        private Repository<Product> _ProductRepository;
        private Repository<Role> _RoleRepository;

        private bool _Disposed = false;

        public UnitOfWork()
        {
            _DbContext = new InventoryDbContext();
        }

        public Repository<Brand> BrandRepository
        {
            get
            {
                if (_BrandRepository == null)
                    _BrandRepository = new Repository<Brand>(_DbContext);

                return _BrandRepository;
            }
        }

        public Repository<Customer> CustomerRepository
        {
            get
            {
                if (_CustomerRepository == null)
                    _CustomerRepository = new Repository<Customer>(_DbContext);

                return _CustomerRepository;
            }
        }

        public Repository<Employee> EmployeeRepository
        {
            get
            {
                if (_EmployeeRepository == null)
                    _EmployeeRepository = new Repository<Employee>(_DbContext);

                return _EmployeeRepository;
            }
        }
        
        public Repository<Invoice> InvoiceRepository
        {
            get
            {
                if (_InvoiceRepository == null)
                    _InvoiceRepository = new Repository<Invoice>(_DbContext);

                return _InvoiceRepository;
            }
        }

        public Repository<InvoiceItem> InvoiceItemRepository
        {
            get
            {
                if (_InvoiceItemRepository == null)
                    _InvoiceItemRepository = new Repository<InvoiceItem>(_DbContext);

                return _InvoiceItemRepository;
            }
        }

        public Repository<Price> PriceRepository
        {
            get
            {
                if (_PriceRepository == null)
                    _PriceRepository = new Repository<Price>(_DbContext);

                return _PriceRepository;
            }
        }

        public Repository<Product> ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                    _ProductRepository = new Repository<Product>(_DbContext);

                return _ProductRepository;
            }
        }

        public Repository<Role> RoleRepository
        {
            get
            {
                if (_RoleRepository == null)
                    _RoleRepository = new Repository<Role>(_DbContext);

                return _RoleRepository;
            }
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _DbContext.SaveChangesAsync();
        }

        public TEntity InsertIfNotExists<TEntity>(IRepository<TEntity> repository, TEntity entity, Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            TEntity loadedEntity = repository.Get(filter).FirstOrDefault();
            if (loadedEntity == null)
            {
                repository.Insert(entity);
            }

            return loadedEntity;
        }

        public void SetEntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class
        {
            _DbContext.Entry(entity).State = entityState;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }

            _Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
