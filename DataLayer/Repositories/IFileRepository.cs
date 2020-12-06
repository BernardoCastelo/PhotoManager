namespace DataLayer
{
    public interface IFileRepository
    {
        File Get(string fullpath);
    }
}