using System.Collections.Generic;

namespace DataLayer
{
    public interface ICameraRepository: IBaseGenericRepository<Camera>
    {
        IEnumerable<Camera> Get(string maker, string model);

    }
}