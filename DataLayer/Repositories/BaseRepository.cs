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
    public abstract class BaseRepository<TTable> : IBaseRepository<TTable>
        where TTable : class, IBaseModel, new()
    {
        protected IDbContainer DbContainer { get; set; }

        protected BaseRepository(IDbContainer dbContainer)
        {
            this.DbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }


        public TTable Select(int id)
        {
            try
            {
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
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
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
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
                return Select(0, 1, property, value);
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
                var queriable = Helper.GetDbSet<TTable>(this.DbContainer).Select(t => t);

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
                var queriable = Select(filterPropertyName, value).AsQueryable();

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
                var queriable = Helper.GetDbSet<TTable>(this.DbContainer).Select(t => t);

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

                return queriable;
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
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
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
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
                dbset.Add(entity);
                var entry = DbContainer.Entry(entity);
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
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
                dbset.Attach(entity);
                var entry = DbContainer.Entry(entity);
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
                var dbset = Helper.GetDbSet<TTable>(this.DbContainer);
                dbset.Attach(entity);
                var entry = DbContainer.Entry(entity);
                entry.State = EntityState.Deleted;
                return entry;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}