namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    public interface ICasinoGameRepository : IDeletableEntityAsyncRepository<CasinoGame>
    {
        Task<IDictionary<string, CasinoGame>> AllAsDictionaryAsync();
    }
}
