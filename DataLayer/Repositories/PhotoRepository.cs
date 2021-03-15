using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(IDbContainer dbContainer) : base(dbContainer)
        { }

        public IEnumerable<Photo> SelectThumbnails(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            var expression = orderBy.GetKeySelected<Photo>();

            var queriable = DbContainer.PhotoSet
                .Select(p => p)
                .Where(photo => photo.Thumbnail != null);

            queriable = orderByDescending ? queriable.OrderByDescending(expression) : queriable.OrderBy(expression);

            return queriable.Skip(skip).Take(take);
        }
    }
}
