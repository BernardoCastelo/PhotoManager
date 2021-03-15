using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public abstract class BaseRepository<TTable> : IBaseRepository<TTable>
        where TTable : class, IBaseModel, new()
    {
        protected IDbContainer<TTable> DbContainer { get; set; }

        protected BaseRepository(IDbContainer<TTable> dbContainer)
        {
            this.DbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }


        public TTable Delete(TTable entity)
        {
            var newEntity = DbContainer.Remove(entity).Entity;
            DbContainer.SaveChanges();
            return newEntity;
        }

        public TTable Insert(TTable entity)
        {
            var newEntity = DbContainer.Add(entity);
            DbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;
        }

        public TTable Select(int id) =>
            DbContainer.Select(id);

        public IEnumerable<TTable> Select(IEnumerable<int> ids) =>
            DbContainer.Select(ids);

        public IEnumerable<TTable> Select(int skip, int take, string orderBy = null, bool orderByDescending = false) =>
            DbContainer.Select(skip, take, orderBy, orderByDescending);

        public IEnumerable<TTable> Select(int skip, int take, string orderBy = null, bool orderByDescending = false, IEnumerable<Filter> filters = null) =>
            DbContainer.Select(skip, take, orderBy, orderByDescending, filters);

        public IEnumerable<TTable> SelectAll() =>
            DbContainer.SelectAll();

        public TTable Update(TTable entity)
        {
            var newEntity = DbContainer.Update(entity);
            DbContainer.SaveChanges();
            newEntity.State = EntityState.Detached;
            return newEntity.Entity;

        }
    }
}