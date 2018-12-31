namespace CasinoReports.Infrastructure.Di
{
    using CasinoReports.Core.Services;
    using CasinoReports.Core.Services.Abstractions;
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
            IConfiguration dataConfiguration)
        {
            BindDbContexts(services, dataConfiguration);
            BindRepositories(services);
            BindServices(services);
        }

        private static void BindDbContexts(IServiceCollection services, IConfiguration dataConfiguration)
        {
            var applicationConnectionString = dataConfiguration.GetConnectionString("ApplicationConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(applicationConnectionString));
        }

        private static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));

            services.AddScoped(typeof(ICasinoGameRepository), typeof(CasinoGameRepository));
            services.AddScoped(typeof(ICasinoPlayerTypeRepository), typeof(CasinoPlayerTypeRepository));
            services.AddScoped(typeof(ICustomerTotalBetRangeRepository), typeof(CustomerTotalBetRangeRepository));
            services.AddScoped(typeof(ICustomerVisitsCollectionRepository), typeof(CustomerVisitsCollectionRepository));
            services.AddScoped(typeof(ICustomerVisitsImportRepository), typeof(CustomerVisitsImportRepository));

            services.AddScoped(typeof(ICustomerVisitsCollectionService), typeof(CustomerVisitsCollectionService));
            services.AddScoped(typeof(ICustomerVisitsImportService), typeof(CustomerVisitsImportService));
        }

        private static void BindServices(IServiceCollection services)
        {
        }
    }
}
