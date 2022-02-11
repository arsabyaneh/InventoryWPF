﻿using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityFramework
{
    public interface IUnitOfWork
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
        void SaveAsync();
        TEntity InsertIfNotExists<TEntity>(IRepository<TEntity> repository, TEntity entity, Expression<Func<TEntity, bool>> filter) where TEntity : class;
        void SetEntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class;
    }
}
