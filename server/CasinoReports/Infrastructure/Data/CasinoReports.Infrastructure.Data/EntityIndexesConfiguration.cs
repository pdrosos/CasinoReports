namespace CasinoReports.Infrastructure.Data
{
    using System.Linq;

    using CasinoReports.Core.Models.Entities;

    using Microsoft.EntityFrameworkCore;

    internal static class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerVisits>()
                .HasIndex(nameof(CustomerVisits.Date));

            // IDeletableEntity.IsDeleted index
            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType)
                    .HasIndex(nameof(IDeletableEntity.IsDeleted));
            }
        }
    }
}
