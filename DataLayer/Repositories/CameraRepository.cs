using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class CameraRepository : BaseRepository<Camera>, ICameraRepository
    {
        public CameraRepository(IDbContainer dbContainer) : base(dbContainer)
        {
            this.DbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public IEnumerable<Camera> Get(string maker, string model)
        {
            try
            {
                return DbContainer.CameraSet.Where(camera =>
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
