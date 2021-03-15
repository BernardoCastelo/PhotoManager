using System.Collections.Generic;

namespace DataLayer
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        IEnumerable<Photo> SelectThumbnails(int skip, int take, string orderBy = null, bool orderByDescending = false);
    }
}