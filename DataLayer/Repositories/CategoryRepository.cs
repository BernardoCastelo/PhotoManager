namespace DataLayer
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContainer dbContainer) : base(dbContainer)
        { }
    }
}
