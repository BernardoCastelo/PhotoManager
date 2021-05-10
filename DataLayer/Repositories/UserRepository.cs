using System;

namespace DataLayer
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContainer dbContainer) : base(dbContainer)
        {
            this.DbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }
    }
}
