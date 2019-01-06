namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using CasinoReports.Core.Models.Entities;

    public interface ICustomerVisitsReportRepository : IDeletableEntityAsyncRepository<CustomerVisitsReport>
    {
    }
}
