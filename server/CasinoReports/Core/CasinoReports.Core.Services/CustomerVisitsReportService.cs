namespace CasinoReports.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Core.Services.Specifications;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsReportService : ICustomerVisitsReportService
    {
        private readonly ICustomerVisitsReportRepository customerVisitsReportRepository;

        public CustomerVisitsReportService(ICustomerVisitsReportRepository customerVisitsReportRepository)
        {
            this.customerVisitsReportRepository = customerVisitsReportRepository;
        }

        public Task<IReadOnlyList<CustomerVisitsReport>> GetAllAsNoTrackingAsync()
        {
            var spec = new AllCustomerVisitsReportsSpecification();

            return this.customerVisitsReportRepository.ListAsNoTrackingAsync(spec);
        }
    }
}
