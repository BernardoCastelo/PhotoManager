using System.Collections.Generic;

namespace DataLayer
{
    public interface ICameraRepository: IBaseRepository<Camera>
    {
        IEnumerable<Camera> Get(string maker, string model);

    }
}