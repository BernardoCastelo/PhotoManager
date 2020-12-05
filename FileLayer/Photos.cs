using DataLayer;
using System;
using System.IO;

namespace BusinessLayer
{
    public class Photos
    {
        private DbContainer dbContainer;

        public Photos(DbContainer dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public Photo Get(string filePath)
        {
            Photo photo = new Photo();
            FileInfo file = new FileInfo(filePath);
            return photo;
        }
        public Photo Get(int id)
        {
            return dbContainer.Select<Photo>(id);
        }
    }
}
