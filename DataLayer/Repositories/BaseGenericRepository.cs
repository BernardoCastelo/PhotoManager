using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public abstract class BaseGenericRepository<T> : IBaseGenericRepository<T> where T : class
    {
        public IDbContainer DbContainer { get; set; }

        protected BaseGenericRepository(IDbContainer dbContainer)
        {
            this.DbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }


        public T Delete(T entity)
        {
            var newEntity = DbContainer.Remove(entity).Entity;
            DbContainer.SaveChanges();
            return newEntity;
        }

        public T Insert(T entity)
        {
            var newEntity = DbContainer.Add(entity);
            DbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;
        }

        public T Select(int id)
        {
            return DbContainer.Select<T>(id);
        }

        public IEnumerable<T> Select(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            return DbContainer.Select<T>(skip, take, orderBy, orderByDescending);
        }

        public IEnumerable<T> Select(int skip, int take, string orderBy = null, bool orderByDescending = false, IEnumerable<Filter> filters = null)
        {
            if(filters != null)
            {
                return DbContainer.Select<T>(skip, take, orderBy, orderByDescending, filters);
            }
            return DbContainer.Select<T>(skip, take, orderBy, orderByDescending);
        }

        public IEnumerable<T> SelectAll()
        {
            return DbContainer.SelectAll<T>();
        }

        public T Update(T entity)
        {
            var newEntity = DbContainer.Update(entity);
            DbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;

        }
    }
}