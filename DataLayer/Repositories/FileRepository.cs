using System;
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
    }
}
