namespace CasinoReports.Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CasinoGameRepository
        : EfDeletableEntityRepository<CasinoGame>, ICasinoGameRepository
    {
        public CasinoGameRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public async Task<IDictionary<string, CasinoGame>> AllAsDictionaryAsync()
        {
            IReadOnlyList<CasinoGame> all = await this.AllAsync();

            var casinoGamesDictionary = new Dictionary<string, CasinoGame>();
            foreach (var casinoGame in all)
            {
                casinoGamesDictionary.Add(casinoGame.Name, casinoGame);
            }

            return casinoGamesDictionary;
        }
    }
}
