using System;
using System.Collections.Generic;

namespace DataLayer
{
    public abstract class BaseGenericRepository<T> : IBaseGenericRepository<T> where T : class
    {
        public IDbContainer dbContainer { get; set; }

        protected BaseGenericRepository(IDbContainer dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public T Delete(T entity)
        {
            var newEntity = dbContainer.Remove(entity).Entity;
            dbContainer.SaveChanges();
            return newEntity;
        }
        public T Insert(T entity)
        {
            var newEntity = dbContainer.Add(entity).Entity;
            dbContainer.SaveChanges();
            return newEntity;
        }
        public T Select(int id)
        {
            return dbContainer.Select<T>(id);
        }
        public IEnumerable<T> Select(int skip, int take)
        {
            return dbContainer.Select<T>(skip, take);
        }
        public IEnumerable<T> SelectAll()
        {
            return dbContainer.SelectAll<T>();
        }
        public T Update(T entity)
        {
            var newEntity = dbContainer.Update(entity).Entity;
            dbContainer.SaveChanges();
            return newEntity;

        }
    }
}