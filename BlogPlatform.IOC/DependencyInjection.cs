using BlogPlatform.Application.Interfaces.Repositories;
using BlogPlatform.Application.Interfaces.Services;
using BlogPlatform.Application.Profiles;
using BlogPlatform.Application.Services;
using BlogPlatform.Infrastructure.Configuration;
using BlogPlatform.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogPlatform.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.ConfigureDB(configuration);
            services.AddRepositories();
            services.AddServices();

            return services;
        }

        private static void ConfigureDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBaseSqlLiteServer(configuration["ConnectionStrings:DefaultConnection"]!);

            var serviceProvider = services.BuildServiceProvider();
            DbContextInitialize.ApplyMigrations(serviceProvider);
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            return services;
        }
    }
}
