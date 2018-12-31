namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    public interface ICustomerTotalBetRangeRepository : IDeletableEntityAsyncRepository<CustomerTotalBetRange>
    {
        Task<IDictionary<string, CustomerTotalBetRange>> AllAsDictionaryAsync();
    }
}
