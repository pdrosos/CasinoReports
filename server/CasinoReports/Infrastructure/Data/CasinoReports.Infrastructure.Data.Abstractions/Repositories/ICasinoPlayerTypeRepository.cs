namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    public interface ICasinoPlayerTypeRepository : IDeletableEntityAsyncRepository<CasinoPlayerType>
    {
        Task<IDictionary<string, CasinoPlayerType>> AllAsDictionaryAsync();
    }
}
