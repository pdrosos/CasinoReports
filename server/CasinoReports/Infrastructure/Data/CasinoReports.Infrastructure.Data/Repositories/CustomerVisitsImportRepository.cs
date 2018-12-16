namespace CasinoReports.Infrastructure.Data.Repositories
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsImportRepository
        : EfDeletableEntityRepository<CustomerVisitsImport>, ICustomerVisitsImportRepository
    {
        public CustomerVisitsImportRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
