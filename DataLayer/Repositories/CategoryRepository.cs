namespace DataLayer
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContainer<Category> dbContainer) : base(dbContainer)
        { }
    }
}
