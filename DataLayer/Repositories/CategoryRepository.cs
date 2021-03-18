using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContainer dbContainer) 
            : base(dbContainer)
        { }

        public IEnumerable<Category> GetByPhotoId(int photoId)
        {
            try
            {
                return DbContainer.PhotoCategorySet
                    .Where(photoCategory=> photoCategory.PhotoId == photoId)
                    .Join(DbContainer.CategorySet,
                      photoCategory => photoCategory.CategoryId,
                      category => category.Id,
                      (photoCategory, category) => category)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
