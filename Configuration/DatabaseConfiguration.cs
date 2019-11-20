using System;
using System.Reflection;
using AreYouOk.Database.Repositories;
using AreYouOk.Database.Repositories.PostgreSQL.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AreYouOk.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string dbConnectionString, string provider)
        {
            switch (provider.ToLower())
            {
                case "postgres":
                    ConfigurePostgres(services, dbConnectionString);
                    break;
                case "mssql":
                    ConfigureMssql(services, dbConnectionString);
                    break;
                case "mongodb":
                    ConfigureMongo(services, dbConnectionString);
                    break;
                default:
                    throw new ArgumentException($"No implementation for provider {provider}.");
            }

            return services;
        }

        private static IServiceCollection ConfigurePostgres(IServiceCollection services, string dbConnectionString)
        {
            var assembly = Assembly.GetAssembly(typeof(Initial));
            var assemblyString = assembly.ToString();
            services
                 .AddEntityFrameworkNpgsql()
                 .AddDbContext<Database.Repositories.PostgreSQL.DataContext>(
                     options => options.UseNpgsql(dbConnectionString,
                     optionsAction => optionsAction.MigrationsAssembly(assemblyString))
                         .UseSnakeCaseNamingConvention());

            services.AddScoped<IHealthRepository, Database.Repositories.PostgreSQL.HealthRepository>();
            return services;
        }

        private static void ConfigureMongo(IServiceCollection services, string dbConnectionString)
        {
            throw new NotImplementedException();
        }

        private static void ConfigureMssql(IServiceCollection services, string dbConnectionString)
        {
            throw new NotImplementedException();
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, string provider)
        {
            switch (provider.ToLower())
            {
                case "postgres":
                    MigratePostgres(app);
                    break;
                case "mssql":
                    MigrateMssql(app);
                    break;
                case "mongodb":
                    MigrateMongo(app);
                    break;
                default:
                    throw new ArgumentException($"No implementation for provider {provider}.");
            }

            return app;
        }

        private static IApplicationBuilder MigratePostgres(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<Database.Repositories.PostgreSQL.DataContext>().Database;
                database.Migrate();
            }

            return app;
        }
        private static IApplicationBuilder MigrateMongo(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
        private static IApplicationBuilder MigrateMssql(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}