using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class Generics
    {
        public static DbSet<T> GetProperty<T>(object o) where T : class
        {
            return (DbSet<T>)o
                .GetType()
                .GetProperty($"{nameof(T)}Set")
                .GetMethod
                .Invoke(o, null);
        }
    }
}
