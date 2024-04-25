using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Context;
using Repository.Interfaces.Base;

namespace Repository.Implementations.Base
{
    internal class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public RepositoryBase(ExchnageContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        protected ExchnageContext RepositoryContext { get; set; }


        public IQueryable<TEntity> FindAll()
        {
            return RepositoryContext.Set<TEntity>().AsNoTracking();
        }


        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public IQueryable<TEntity> FindByConditionWithTracking(Expression<Func<TEntity, bool>> expression)
        {
            return RepositoryContext.Set<TEntity>().Where(expression);
        }

        public void Create(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Add(entity);
        }
        public void AddRange(List<TEntity> entities)
        {
            this.RepositoryContext.Set<TEntity>().AddRange(entities);
        }
        public void UpdateRange(List<TEntity> entities)
        {
            this.RepositoryContext.Set<TEntity>().UpdateRange(entities);
        }
        public void Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Remove(entity);
        }
    }
}

