using Common;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class Generics
    {
        public static DbSet<T> GetDbSet<T>(object obj) where T : class
        {
            return (DbSet<T>)Extentions.GetProperty(obj, $"{typeof(T).Name}Set");
        }
    }
}
