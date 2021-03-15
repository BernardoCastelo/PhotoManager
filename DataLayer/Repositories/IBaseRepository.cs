using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IBaseRepository<TTable>
        where TTable : class, IBaseModel, new()
    {
        EntityEntry<TTable> Add(TTable entity);
        EntityEntry<TTable> Remove(TTable entity);
        TTable Select(int id);
        IEnumerable<TTable> Select(IEnumerable<int> ids);
        IEnumerable<TTable> Select<TProp>(string property, TProp value) where TProp : class;
        IEnumerable<TTable> Select(int skip, int take, string orderByPropName, bool descending = false);
        IEnumerable<TTable> Select<TProp>(int skip, int take, string filterPropertyName, TProp value, string orderByPropName = "Id", bool descending = false) where TProp : class;
        IEnumerable<TTable> Select(int skip, int take, string orderByPropName, bool descending, IEnumerable<Filter> filters);
        IEnumerable<TTable> SelectAll();
        EntityEntry<TTable> Update(TTable entity);
    }
}