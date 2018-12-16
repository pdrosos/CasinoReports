namespace CasinoReports.Infrastructure.Di
{
    using CasinoReports.Infrastructure.Data;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;
    using CasinoReports.Infrastructure.Data.Repositories;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            BindDbContexts(services, configuration);
            BindRepositories(services);
            BindServices(services);
        }

        private static void BindDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            var applicationConnectionString = configuration.GetConnectionString("ApplicationConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(applicationConnectionString));
        }

        private static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(ICustomerVisitsCollectionRepository), typeof(CustomerVisitsCollectionRepository));
            services.AddScoped(typeof(ICustomerVisitsImportRepository), typeof(CustomerVisitsImportRepository));
        }

        private static void BindServices(IServiceCollection services)
        {
        }
    }
}
