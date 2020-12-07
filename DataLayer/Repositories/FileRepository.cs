using System;
using System.Linq;

namespace DataLayer
{
    public class FileRepository : BaseGenericRepository<File>, IFileRepository
    {
        public FileRepository(IDbContainer dbContainer) : base(dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public File Select(string fullpath)
        {
            try
            {
                return dbContainer.FileSet.Where(file => file.Fullpath == fullpath).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
