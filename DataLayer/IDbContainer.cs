using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer
{
    interface IDbContainer
    {
        DbSet<Camera> CameraSet { get; set; }
        DbSet<Category> CategorySet { get; set; }
        DbSet<File> FileSet { get; set; }
        DbSet<FileType> FileTypeSet { get; set; }
        DbSet<Folder> FolderSet { get; set; }
        DbSet<Photo> PhotoSet { get; set; }

        int SaveChanges();
        EntityEntry<T> Add<T>(T entity) where T : class;
        EntityEntry<T> Update<T>(T entity) where T : class;
    }
}