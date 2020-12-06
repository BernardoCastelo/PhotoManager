using System.Collections.Generic;

namespace DataLayer
{
    public interface IBaseGenericRepository<T> where T : class
    {
        IDbContainer dbContainer { get; set; }

        T Delete(T entity);
        T Insert(T entity);
        T Select(int id);
        IEnumerable<T> Select(int skip, int take);
        IEnumerable<T> SelectAll();
        T Update(T entity);
    }
}