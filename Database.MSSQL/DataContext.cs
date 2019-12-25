using Database.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.MSSQL
{
    public class DataContext : DbContext
    {
        public DbSet<HealthModel> HealthData { get; set; }
        public DbSet<EndpointModel> Endpoints { get; set; }
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthModel>().HasKey(h => h.Timestamp);
        }
    }
}
