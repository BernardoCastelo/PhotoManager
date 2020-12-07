namespace DataLayer
{
    public interface IFileRepository: IBaseGenericRepository<File>
    {
        File Select(string fullpath);
    }
}