namespace CasinoReports.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Core.Services.Specifications;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsImportService : ICustomerVisitsImportService
    {
        private ICustomerVisitsImportRepository customerVisitsImportRepository;

        public CustomerVisitsImportService(ICustomerVisitsImportRepository customerVisitsImportRepository)
        {
            this.customerVisitsImportRepository = customerVisitsImportRepository;
        }

        public Task<IReadOnlyList<CustomerVisitsImport>> GetAllWithCollectionAsNoTrackingAsync()
        {
            var spec = new AllCustomerVisitsImportsWithCollectionSpecification();

            return this.customerVisitsImportRepository.ListAsNoTrackingAsync(spec);
        }
    }
}
