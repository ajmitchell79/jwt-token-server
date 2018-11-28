using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Token.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity> GetById(int id);

        Task<TEntity> GetById(string id);

        Task<TEntity> GetById(Guid id);

        Task Create(TEntity entity);

        Task Update(int id, TEntity entity);

        Task Delete(int id);

        int Count();

        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

    }
}