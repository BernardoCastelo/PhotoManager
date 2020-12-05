using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace PhotoLoader
{
    public class DbConnector
    {
        private DbContainer container;
        public DbContainer Container => container;
        public DbConnector()
        {
            var options = new DbContextOptionsBuilder<DbContainer>();
            options.UseSqlServer("Server=.; Database=PhotoManager; User Id=pmLogin; Password=pmLogin; MultipleActiveResultSets=true; persist security info=True; App=EntityFramework");
            container = new DbContainer(options.Options);
        }
    }
}
