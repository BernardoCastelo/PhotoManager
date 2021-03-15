using DataLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface ICategories
    {
        Category Get(int id);

        IEnumerable<Category> Get(IEnumerable<int> Ids);

        IEnumerable<Category> GetAll();
    }
}