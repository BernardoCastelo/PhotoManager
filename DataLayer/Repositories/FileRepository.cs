using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(IDbContainer dbContainer) : base(dbContainer)
        { }

        public File Select(string fullpath)
        {
            try
            {
                return DbContainer.FileSet.Where(file => file.Fullpath == fullpath).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<File> SelectFullpathContainsText(string text)
        {
            try
            {
                return DbContainer.FileSet.Where(file => file.Fullpath.Contains(text));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
