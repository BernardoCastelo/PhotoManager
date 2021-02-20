using Common;
using DataLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDbContainer
    {
        #region DB Sets
        DbSet<Camera> CameraSet { get; set; }
        DbSet<Category> CategorySet { get; set; }
        DbSet<File> FileSet { get; set; }
        DbSet<FileType> FileTypeSet { get; set; }
        DbSet<Folder> FolderSet { get; set; }
        DbSet<Photo> PhotoSet { get; set; }
        #endregion

        #region DBContainer overrides
        int SaveChanges();
        #endregion

        #region DB Operations

        #region Selects
        TTable Select<TTable>(int id) where TTable : class;
        IEnumerable<TTable> Select<TTable, TProp>(string property, TProp value)
            where TTable : class
            where TProp : class;

        IEnumerable<TTable> SelectAll<TTable>() where TTable : class;
        IEnumerable<TTable> Select<TTable>(int skip, int take, string orderByPropName = Constants.DbConstants.Id, bool descending = false) where TTable : class;
        IEnumerable<TTable> Select<TTable>(int skip, int take, string orderByPropName = Constants.DbConstants.Id, bool descending = false, IEnumerable<Filter> filters = null) where TTable : class;
        IEnumerable<TTable> Select<TTable, TProp>(int skip, int take, string filterPropertyName, TProp value, string orderByPropName = Constants.DbConstants.Id, bool descending = false)
            where TTable : class
            where TProp : class;
        #endregion

        #region OCM
        EntityEntry<TTable> Add<TTable>(TTable entity) where TTable : class;
        EntityEntry<TTable> Remove<TTable>(TTable entity) where TTable : class;
        EntityEntry<TTable> Update<TTable>(TTable entity) where TTable : class;
        #endregion

        #endregion
    }
}