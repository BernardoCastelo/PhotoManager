using System.Collections.Generic;

namespace DataLayer
{
    public interface IPhotoRepository : IBaseGenericRepository<Photo>
    {
        IEnumerable<Photo> SelectThumbnails(int skip, int take, string orderBy = null, bool orderByDescending = false);
    }
}