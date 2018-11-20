namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public interface IDeletableEntityAsyncRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        Task<TEntity> GetByIdWithDeletedAsync(object[] id);

        Task<TEntity> GetSingleBySpecWithDeletedAsync(IQuerySpecification<TEntity> spec);

        Task<IReadOnlyList<TEntity>> AllWithDeletedAsync();

        Task<IReadOnlyList<TEntity>> AllWithDeletedAsNoTrackingAsync();

        Task<IReadOnlyList<TEntity>> ListWithDeletedAsync(IQuerySpecification<TEntity> spec);

        Task<IReadOnlyList<TEntity>> ListWithDeletedAsNoTrackingAsync(IQuerySpecification<TEntity> spec);
    }
}
