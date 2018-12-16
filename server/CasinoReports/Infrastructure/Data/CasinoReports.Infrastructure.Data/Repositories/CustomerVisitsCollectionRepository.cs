namespace CasinoReports.Infrastructure.Data.Repositories
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsCollectionRepository
        : EfDeletableEntityRepository<CustomerVisitsCollection>, ICustomerVisitsCollectionRepository
    {
        public CustomerVisitsCollectionRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
