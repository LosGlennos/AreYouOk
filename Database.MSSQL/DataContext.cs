using Database.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
            modelBuilder.Entity<EndpointModel>().HasKey(e => e.Id);
        }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=areyouok;User Id=sa;Password=password123;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
