using Microsoft.EntityFrameworkCore;
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
            var newEntity = dbContainer.Add(entity);
            dbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;
        }

        public T Select(int id)
        {
            return dbContainer.Select<T>(id);
        }

        public IEnumerable<T> Select(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            return dbContainer.Select<T>(skip, take, orderBy, orderByDescending);
        }

        public IEnumerable<T> SelectAll()
        {
            return dbContainer.SelectAll<T>();
        }

        public T Update(T entity)
        {
            var newEntity = dbContainer.Update(entity);
            dbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;

        }
    }
}