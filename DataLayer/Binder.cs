using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class Binder<T>: IBinder
        where T: class, IBaseModel
    {
        private ModelBuilder modelBuilder;

        public Binder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
        }

        public void Bind()
        {
            modelBuilder.Entity<T>()
                .ToTable(typeof(T).Name)
                .HasKey(key => key.Id);
        }
    }
}
