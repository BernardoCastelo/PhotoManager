using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer
{

    class DbContainer : DbContext, IDbContainer
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

        public override EntityEntry<T> Add<T>(T entity) where T : class
        {
            var dbset = Generics.GetProperty<T>(this);
            dbset.Add(entity);
            var entry = Entry(entity);
            entry.State = EntityState.Added;
            return entry;
        }

        public override EntityEntry<T> Update<T>(T entity) where T : class
        {
            var dbset = Generics.GetProperty<T>(this);
            dbset.Attach(entity);
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
            return entry;
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
