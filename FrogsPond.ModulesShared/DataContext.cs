using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
 
namespace FrogsPond.ModulesShared
{
    public class DataContext : DbContext
    {
        //public DbSet<Account> Accounts { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            //options.UseSqlite(Configuration.GetConnectionString("FrogPondDB"));
        }
    }
}
