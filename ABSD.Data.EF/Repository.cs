using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ABSD.Data.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    items = items.Include(property);
                }
            }

            return items.SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    items = items.Include(property);
                }
            }

            return items.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    items = items.Include(property);
                }
            }

            return items;
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    items = items.Include(property);
                }
            }

            return items.Where(predicate);
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            context.AddRange(entities);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}