using Common;
using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Constants;

namespace DataLayer
{

    public class DbContainer<TTable> : DbContext, IDbContainer<TTable>
        where TTable : class, IBaseModel, new()
    {
        protected DbSet<Photo> PhotoSet { get; set; }
        protected DbSet<Folder> FolderSet { get; set; }
        protected DbSet<File> FileSet { get; set; }
        protected DbSet<FileType> FileTypeSet { get; set; }
        protected DbSet<Category> CategorySet { get; set; }
        protected DbSet<Camera> CameraSet { get; set; }

        public DbContainer(DbContextOptions<DbContainer<TTable>> options) 
            : base(options)
        {
        }

        public TTable Select(int id)
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                return dbset.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TTable> Select(IEnumerable<int> ids)
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                return dbset.Where(table => ids.Contains(table.Id));
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Select with Filter
        public IEnumerable<TTable> Select<TProp>(string property, TProp value)
            where TProp : class
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                return dbset.Where(t => (TProp)t.GetProperty(property, null) == value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Select with OrderBy
        public IEnumerable<TTable> Select(int skip, int take, string orderByPropName, bool descending = false) 
        {
            try
            {
                var queriable = Helper.GetDbSet<TTable>(this).Select(t => t);

                var sortExpression = orderByPropName.GetKeySelected<TTable>();

                queriable = descending ? queriable.OrderByDescending(sortExpression) : queriable.OrderBy(sortExpression);

                return queriable.Skip(skip).Take(take);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Select with OrderBy
        public IEnumerable<TTable> Select<TProp>(int skip, int take, string filterPropertyName, TProp value, string orderByPropName = Constants.DbConstants.Id, bool descending = false)
            where TProp : class
        {
            try
            {
                var queriable = Select<TProp>(filterPropertyName, value).AsQueryable();

                var sortExpression = orderByPropName.GetKeySelected<TTable>();

                queriable = descending ? queriable.OrderByDescending(sortExpression) : queriable.OrderBy(sortExpression);

                return queriable.Skip(skip).Take(take);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Select with OrderBy and filters
        public IEnumerable<TTable> Select(int skip, int take, string orderByPropName, bool descending, IEnumerable<Filter> filters) 
        {
            try
            {
                var queriable = Helper.GetDbSet<TTable>(this).Select(t => t);

                foreach (var filter in filters)
                {
                    if (!filter.Negate)
                    {
                        if (filter.LowerValue == null && filter.UpperValue == null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.Value, WhereConditions.Equal));
                        }
                        if (filter.LowerValue != null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.LowerValue, WhereConditions.GreaterOrEqualThan));
                        }
                        if (filter.UpperValue != null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.UpperValue, WhereConditions.LessOrEqualThan));
                        }
                    }
                    else
                    {
                        if (filter.LowerValue == null && filter.UpperValue == null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.Value, WhereConditions.NotEqual));
                        }

                        if (filter.LowerValue != null && filter.UpperValue != null)
                        {
                            queriable = queriable.Where(
                                filter.FieldName.GetExpression<TTable>(filter.LowerValue, WhereConditions.LessOrEqualThan)
                                .Or(filter.FieldName.GetExpression<TTable>(filter.UpperValue, WhereConditions.GreaterOrEqualThan)));
                        }

                        if (filter.LowerValue == null && filter.UpperValue != null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.UpperValue, WhereConditions.GreaterOrEqualThan));
                        }

                        if (filter.LowerValue != null && filter.UpperValue == null)
                        {
                            queriable = queriable.Where(filter.FieldName.GetExpression<TTable>(filter.UpperValue, WhereConditions.LessOrEqualThan));
                        }
                    }
                }

                var sortExpression = orderByPropName.GetKeySelected<TTable>();

                queriable = descending ? queriable.OrderByDescending(sortExpression) : queriable.OrderBy(sortExpression);

                return queriable.Skip(skip).Take(take);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TTable> SelectAll() 
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                return dbset.Select(t => t).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityEntry<TTable> Add(TTable entity) 
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                dbset.Add(entity);
                var entry = Entry(entity);
                entry.State = EntityState.Added;
                return entry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityEntry<TTable> Update(TTable entity) 
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                dbset.Attach(entity);
                var entry = Entry(entity);
                entry.State = EntityState.Modified;
                return entry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityEntry<TTable> Remove(TTable entity) 
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this);
                dbset.Attach(entity);
                var entry = Entry(entity);
                entry.State = EntityState.Deleted;
                return entry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Binder<Photo>(modelBuilder).Bind();
            new Binder<Folder>(modelBuilder).Bind();
            new Binder<File>(modelBuilder).Bind();
            new Binder<FileType>(modelBuilder).Bind();
            new Binder<Category>(modelBuilder).Bind();
            new Binder<Camera>(modelBuilder).Bind();
        }
    }
}
