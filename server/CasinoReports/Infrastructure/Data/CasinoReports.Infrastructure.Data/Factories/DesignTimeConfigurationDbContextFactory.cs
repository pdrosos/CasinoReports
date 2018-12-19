namespace CasinoReports.Infrastructure.Data.Factories
{
    using System.IO;

    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Options;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("datasettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ApplicationConnection");

            var migrationsAssembly = typeof(DesignTimeConfigurationDbContextFactory).Assembly.GetName().Name;

            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly));

            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions());
        }
    }
}
