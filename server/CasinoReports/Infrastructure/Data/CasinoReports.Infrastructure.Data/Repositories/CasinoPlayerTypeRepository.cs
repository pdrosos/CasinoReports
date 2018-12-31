namespace CasinoReports.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CasinoPlayerTypeRepository
        : EfDeletableEntityRepository<CasinoPlayerType>, ICasinoPlayerTypeRepository
    {
        public CasinoPlayerTypeRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public async Task<IDictionary<string, CasinoPlayerType>> AllAsDictionaryAsync()
        {
            IReadOnlyList<CasinoPlayerType> all = await this.AllAsync();

            var casinoPlayerTypesDictionary = new Dictionary<string, CasinoPlayerType>();
            foreach (var casinoPlayerType in all)
            {
                casinoPlayerTypesDictionary.Add(casinoPlayerType.Name, casinoPlayerType);
            }

            return casinoPlayerTypesDictionary;
        }
    }
}
