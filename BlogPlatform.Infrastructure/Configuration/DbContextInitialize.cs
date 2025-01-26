using BlogPlatform.Domain.Entities;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogPlatform.Infrastructure.Configuration
{
    public static class DbContextInitialize
    {
        public static IServiceCollection AddDataBaseSqlLiteServer(this IServiceCollection services, string connectionString)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                })
            );

            services.AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString) 
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()) 
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }

        public static void ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
