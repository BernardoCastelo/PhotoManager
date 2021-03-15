namespace DataLayer
{
    public class FileTypeRepository : BaseRepository<FileType>, IFileTypeRepository
    {
        public FileTypeRepository(IDbContainer dbContainer) : base(dbContainer)
        { }


    }
}
