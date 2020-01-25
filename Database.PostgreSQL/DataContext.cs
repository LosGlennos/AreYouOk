using Database.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.PostgreSQL
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
            modelBuilder.Entity<EndpointModel>().HasNoKey();
        }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=areyouok;");
            optionsBuilder.UseSnakeCaseNamingConvention();

            return new DataContext(optionsBuilder.Options);
        }
    }
}
