using AreYouOk.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AreYouOk.Database.Repositories.MSSQL
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
}
