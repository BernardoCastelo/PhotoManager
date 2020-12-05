using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{

    public class DbContainer : DbContext, IDbContainer
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

        public T Select<T>(int id) where T : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                return Select<T>("Id", id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Select<T>(string property, int number) where T : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                return dbset.Where(t => (int)t.GetProperty(property, null) == number).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Select<T, T2>(string property, T2 value)
            where T : class 
            where T2 : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                return dbset.Where(t => (T2)t.GetProperty(property, null) == value).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                return dbset.Select(t => t).ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public override EntityEntry<T> Add<T>(T entity) where T : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                dbset.Add(entity);
                var entry = Entry(entity);
                entry.State = EntityState.Added;
                return entry;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public override EntityEntry<T> Update<T>(T entity) where T : class
        {
            try
            {
                var dbset = Generics.GetDbSet<T>(this);
                dbset.Attach(entity);
                var entry = Entry(entity);
                entry.State = EntityState.Modified;
                return entry;
            }
            catch (System.Exception)
            {
                throw;
            }
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
