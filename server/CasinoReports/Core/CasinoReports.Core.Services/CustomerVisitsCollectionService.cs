namespace CasinoReports.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Core.Services.Specifications;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsCollectionService : ICustomerVisitsCollectionService
    {
        private readonly ICustomerVisitsCollectionRepository customerVisitsCollectionRepository;

        public CustomerVisitsCollectionService(ICustomerVisitsCollectionRepository customerVisitsCollectionRepository)
        {
            this.customerVisitsCollectionRepository = customerVisitsCollectionRepository;
        }

        public Task<IReadOnlyList<CustomerVisitsCollection>> GetAllAsNoTrackingAsync()
        {
            var spec = new AllCustomerVisitsCollectionsSpecification();

            return this.customerVisitsCollectionRepository.ListAsNoTrackingAsync(spec);
        }
    }
}
