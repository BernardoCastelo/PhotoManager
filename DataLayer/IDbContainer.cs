using Common;
using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDbContainer<TTable>
        where TTable : class, IBaseModel, new()
    {

        #region DBContainer overrides
        int SaveChanges();
        #endregion

        #region DB Operations

        #region Selects
        TTable Select(int id);
        IEnumerable<TTable> Select(IEnumerable<int> ids);
        IEnumerable<TTable> Select<TProp>(string property, TProp value)
            where TProp : class;

        IEnumerable<TTable> SelectAll();
        IEnumerable<TTable> Select(int skip, int take, string orderByPropName = Constants.DbConstants.Id, bool descending = false);
        IEnumerable<TTable> Select(int skip, int take, string orderByPropName = Constants.DbConstants.Id, bool descending = false, IEnumerable<Filter> filters = null);
        IEnumerable<TTable> Select<TProp>(int skip, int take, string filterPropertyName, TProp value, string orderByPropName = Constants.DbConstants.Id, bool descending = false)
            where TProp : class;
        #endregion

        #region OCM
        EntityEntry<TTable> Add(TTable entity);
        EntityEntry<TTable> Remove(TTable entity);
        EntityEntry<TTable> Update(TTable entity);
        #endregion

        #endregion
    }
}