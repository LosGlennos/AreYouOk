using System;
using System.Reflection;
using Core.OutboundPorts.Repositories;
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
            var assembly = Assembly.GetAssembly(typeof(Database.PostgreSQL.Migrations.Initial));
            var assemblyString = assembly.ToString();
            services
                 .AddEntityFrameworkNpgsql()
                 .AddDbContext<Database.PostgreSQL.DataContext>(
                     options => options.UseNpgsql(dbConnectionString,
                     optionsAction => optionsAction.MigrationsAssembly(assemblyString))
                         .UseSnakeCaseNamingConvention());

            services.AddScoped<IHealthRepository, Database.PostgreSQL.Repositories.HealthRepository>();
            services.AddScoped<IEndpointsRepository, Database.PostgreSQL.Repositories.EndpointsRepository>();
            return services;
        }

        private static void ConfigureMongo(IServiceCollection services, string dbConnectionString)
        {
            services.AddSingleton(new Database.MongoDB.DataContext(dbConnectionString));
            services.AddScoped<IHealthRepository, Database.MongoDB.Repositories.HealthRepository>();
            services.AddScoped<IEndpointsRepository, Database.MongoDB.Repositories.EndpointsRepository>();
        }

        private static IServiceCollection ConfigureMssql(IServiceCollection services, string dbConnectionString)
        {
            var assembly = Assembly.GetAssembly(typeof(Database.MSSQL.Migrations.Initial));
            var assemblyString = assembly.ToString();

            services
                .AddDbContext<Database.MSSQL.DataContext>(
                    options => options.UseSqlServer(dbConnectionString, 
                        optionsAction => optionsAction.MigrationsAssembly(assemblyString)));

            services.AddScoped<IHealthRepository, Database.MSSQL.Repositories.HealthRepository>();
            services.AddScoped<IEndpointsRepository, Database.MSSQL.Repositories.EndpointsRepository>();
            return services;
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
                var database = scope.ServiceProvider.GetRequiredService<Database.PostgreSQL.DataContext>().Database;
                database.Migrate();
            }

            return app;
        }
        private static IApplicationBuilder MigrateMongo(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<Database.MongoDB.DataContext>();
                database.Migrate();
            }

            return app;
        }
        private static IApplicationBuilder MigrateMssql(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<Database.MSSQL.DataContext>().Database;
                database.Migrate();
            }

            return app;
        }
    }
}