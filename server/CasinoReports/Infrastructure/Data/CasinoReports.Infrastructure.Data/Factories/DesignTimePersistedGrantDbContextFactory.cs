namespace CasinoReports.Infrastructure.Data.Factories
{
    using System.IO;

    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Options;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimePersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("datasettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ApplicationConnection");

            var migrationsAssembly = typeof(DesignTimePersistedGrantDbContextFactory).Assembly.GetName().Name;

            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly));

            return new PersistedGrantDbContext(builder.Options, new OperationalStoreOptions());
        }
    }
}
