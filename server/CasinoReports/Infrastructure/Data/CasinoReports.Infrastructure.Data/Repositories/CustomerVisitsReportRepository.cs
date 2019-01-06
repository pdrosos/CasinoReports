namespace CasinoReports.Infrastructure.Data.Repositories
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsReportRepository
        : EfDeletableEntityRepository<CustomerVisitsReport>, ICustomerVisitsReportRepository
    {
        public CustomerVisitsReportRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
