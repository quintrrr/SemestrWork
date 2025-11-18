using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext 
    {
        public DbSet<TGroup> Groups { get; set; }
        public DbSet<TRelation> Relations { get; set; }
        public DbSet<TProperty> Properties { get; set; }

        public AppDbContext(string connectionString) : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options) { }
    }
}
