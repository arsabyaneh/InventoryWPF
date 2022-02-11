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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal InventoryDbContext _DbContext;
        internal DbSet<TEntity> _DbSet;

        public Repository(InventoryDbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return _DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _DbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = _DbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_DbContext.Entry(entity).State == EntityState.Detached)
            {
                _DbSet.Attach(entity);
            }
            _DbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_DbContext.Entry(entity).State == EntityState.Detached)
                {
                    _DbSet.Attach(entity);
                }
            }
            _DbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _DbSet.Update(entity);
        }
    }
}
