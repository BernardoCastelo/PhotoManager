namespace DataLayer
{
    public interface IFileRepository: IBaseRepository<File>
    {
        File Select(string fullpath);
    }
}