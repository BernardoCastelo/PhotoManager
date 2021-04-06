using System.Collections.Generic;

namespace DataLayer
{
    public interface IFileRepository: IBaseRepository<File>
    {
        File Select(string fullpath);
        IEnumerable<File> SelectFullpathContainsText(string text);
    }
}