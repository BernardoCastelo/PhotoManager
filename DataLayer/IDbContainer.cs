using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

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
        IEnumerable<T> GetAll<T>() where T : class;
        T Select<T>(string property, int number) where T : class;
        T Select<T, T2>(string property, T2 value)
            where T : class
            where T2 : class;
        T Select<T>(int id) where T : class;
    }
}