using System.Collections.Generic;

namespace DataLayer
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetByPhotoId(int photoId);
    }
}