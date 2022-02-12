using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetDbSet();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
