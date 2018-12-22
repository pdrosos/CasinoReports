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
        private readonly ICustomerVisitsImportRepository customerVisitsImportRepository;

        public CustomerVisitsImportService(ICustomerVisitsImportRepository customerVisitsImportRepository)
        {
            this.customerVisitsImportRepository = customerVisitsImportRepository;
        }

        public Task<IReadOnlyList<CustomerVisitsImport>> GetAllWithCollectionsAsNoTrackingAsync()
        {
            var spec = new AllCustomerVisitsImportsWithCollectionsSpecification();

            return this.customerVisitsImportRepository.ListAsNoTrackingAsync(spec);
        }

        public Task<int> CreateAsync(
            string name,
            IEnumerable<int> customerVisitsCollectionIds,
            IEnumerable<CustomerVisits> customerVisits)
        {
            CustomerVisitsImport customerVisitsImport = new CustomerVisitsImport(name);

            foreach (int customerVisitsCollectionId in customerVisitsCollectionIds)
            {
                var customerVisitsCollectionImport = new CustomerVisitsCollectionImport(
                    customerVisitsCollectionId,
                    customerVisitsImport);

                customerVisitsImport.AddCustomerVisitsCollectionImport(customerVisitsCollectionImport);
            }

            foreach (CustomerVisits visits in customerVisits)
            {
                customerVisitsImport.AddCustomerVisits(visits);
            }

            this.customerVisitsImportRepository.Add(customerVisitsImport);

            return this.customerVisitsImportRepository.SaveChangesAsync();
        }
    }
}
