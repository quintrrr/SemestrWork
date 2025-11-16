using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppContext : DbContext 
    {
        public DbSet<TGroup> Groups { get; set; }
        public DbSet<TRelation> Relations { get; set; }
        public DbSet<TProperty> Properties { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
