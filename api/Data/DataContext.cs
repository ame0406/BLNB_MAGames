using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Condition> Condition { get; set; }
        public DbSet<ConsoleSystem> ConsoleSystem { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Lots> Lots { get; set; }
        public DbSet<SaleType> SaleType { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<VenteMKP> VenteMKP { get; set; }
        public DbSet<VenteEbay> VenteEbay { get; set; }
    }
}
