namespace CasinoReports.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod = typeof(ApplicationDbContext).GetMethod(
            nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Casino> Casinos { get; set; }

        public DbSet<CasinoGame> CasinoGames { get; set; }

        public DbSet<CasinoManager> CasinoManagers { get; set; }

        public DbSet<CasinoPlayerType> CasinoPlayerTypes { get; set; }

        public DbSet<CustomerTotalBetRange> CustomerTotalBetRanges { get; set; }

        public DbSet<CustomerVisits> CustomerVisits { get; set; }

        public DbSet<CustomerVisitsCollection> CustomerVisitsCollections { get; set; }

        public DbSet<CustomerVisitsCollectionCasino> CustomerVisitsCollectionCasinos { get; set; }

        public DbSet<CustomerVisitsCollectionImport> CustomerVisitsCollectionImports { get; set; }

        public DbSet<CustomerVisitsCollectionUser> CustomerVisitsCollectionUsers { get; set; }

        public DbSet<CustomerVisitsImport> CustomerVisitsImports { get; set; }

        public DbSet<CustomerVisitsReport> CustomerVisitsReports { get; set; }

        public override int SaveChanges()
        {
            return this.SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(true, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser identity relations
            ApplicationUserIdentityRelationsConfiguration.Configure(modelBuilder);

            // Configure entity properties
            EntityPropertiesConfiguration.Configure(modelBuilder);

            // Configure entity indexes
            EntityIndexesConfiguration.Configure(modelBuilder);

            // Set global query filter for not deleted entities only
            List<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            IEnumerable<IMutableEntityType> deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                MethodInfo method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { modelBuilder });
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder modelBuilder)
            where T : class, IDeletableEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditableEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
