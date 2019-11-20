using AreYouOk.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AreYouOk.Database.Repositories.PostgreSQL
{
    public class DataContext : DbContext
    {
        public DbSet<HealthModel> HealthData { get; set; }
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthModel>().HasKey(h => h.Timestamp);
        }
    }

    /// <summary>
    /// Used by "dotnet ef migrations" command to perform comparisons to the existing database.
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder
                .UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=areyouok;")
                .UseSnakeCaseNamingConvention();

            return new DataContext(optionsBuilder.Options);
        }
    }
}
