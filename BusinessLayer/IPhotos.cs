using DataLayer;
using DataLayer.Dtos;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IPhotos
    {
        Photo Get(int id);
        IEnumerable<Photo> Get(int skip, int take, string orderBy = null, bool orderByDescending = false);
        IEnumerable<Photo> Get(int skip, int take, string orderBy = null, bool orderByDescending = false, List<Filter> filters = null);
        string GetBytes(int id);
        string GetFullPath(int id);
        Photo Load(string filepath);
    }
}