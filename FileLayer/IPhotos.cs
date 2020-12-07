using DataLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IPhotos
    {
        Photo Get(int id);
        IEnumerable<Photo> Get(int skip, int take);
        string GetBytes(int id);
        Photo Load(string filepath);
    }
}