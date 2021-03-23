using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer
{
    public interface IDbContainer
    {
        DbSet<Photo> PhotoSet { get; set; }
        DbSet<Folder> FolderSet { get; set; }
        DbSet<File> FileSet { get; set; }
        DbSet<FileType> FileTypeSet { get; set; }
        DbSet<Category> CategorySet { get; set; }
        DbSet<Camera> CameraSet { get; set; }

        DbSet<PhotoCategory> PhotoCategorySet { get; set; }

        #region DBContainer overrides
        int SaveChanges();
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        #endregion
    }
}