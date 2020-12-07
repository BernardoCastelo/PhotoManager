using System;

namespace DataLayer
{
    public class PhotoRepository : BaseGenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(IDbContainer dbContainer) : base(dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }
    }
}
