using System;
using System.Linq;

namespace DataLayer
{
    public class PhotoRepository : BaseGenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContainer dbContainer) : base(dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }
    }
}
