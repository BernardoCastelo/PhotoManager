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

    public class DbContainer : DbContext, IDbContainer
    {
        public DbSet<Photo> PhotoSet { get; set; }
        public DbSet<Folder> FolderSet { get; set; }
        public DbSet<File> FileSet { get; set; }
        public DbSet<FileType> FileTypeSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Camera> CameraSet { get; set; }
        public DbContainer(DbContextOptions<DbContainer> options) : base(options)
        {
        }

        public TTable Select<TTable>(int id) where TTable : class
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


        // Select with Filter
        public IEnumerable<TTable> Select<TTable, TProp>(string property, TProp value)
            where TTable : class
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
        public IEnumerable<TTable> Select<TTable>(int skip, int take, string orderByPropName, bool descending = false) where TTable : class
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
        public IEnumerable<TTable> Select<TTable, TProp>(int skip, int take, string filterPropertyName, TProp value, string orderByPropName = Constants.DbConstants.Id, bool descending = false)
            where TTable : class
            where TProp : class
        {
            try
            {
                var queriable = Select<TTable, TProp>(filterPropertyName, value).AsQueryable();

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
        public IEnumerable<TTable> Select<TTable>(int skip, int take, string orderByPropName, bool descending, IEnumerable<Filter> filters) where TTable : class
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

        public IEnumerable<TTable> SelectAll<TTable>() where TTable : class
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

        public override EntityEntry<TTable> Add<TTable>(TTable entity) where TTable : class
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

        public override EntityEntry<TTable> Update<TTable>(TTable entity) where TTable : class
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

        public override EntityEntry<TTable> Remove<TTable>(TTable entity) where TTable : class
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
