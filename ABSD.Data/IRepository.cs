using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ABSD.Data
{
    public interface IRepository<T> where T : class
    {
        T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);

        void AddRange(List<T> entities);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(List<T> entities);
    }
}