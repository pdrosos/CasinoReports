namespace CasinoReports.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerTotalBetRangeRepository
        : EfDeletableEntityRepository<CustomerTotalBetRange>, ICustomerTotalBetRangeRepository
    {
        public CustomerTotalBetRangeRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public async Task<IDictionary<string, CustomerTotalBetRange>> AllAsDictionaryAsync()
        {
            IReadOnlyList<CustomerTotalBetRange> all = await this.AllAsync();

            var customerTotalBetRangesDictionary = new Dictionary<string, CustomerTotalBetRange>();
            foreach (var customerTotalBetRange in all)
            {
                customerTotalBetRangesDictionary.Add(customerTotalBetRange.Name, customerTotalBetRange);
            }

            return customerTotalBetRangesDictionary;
        }
    }
}
