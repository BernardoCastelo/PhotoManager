using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class CameraRepository : BaseGenericRepository<Camera>, ICameraRepository
    {
        public CameraRepository(DbContainer dbContainer) : base(dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public IEnumerable<Camera> Get(string maker, string model)
        {
            try
            {
                return dbContainer.CameraSet.Where(camera =>
                    (camera.Maker == maker || maker == null) &&
                    (camera.Model == model || model == null))
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
