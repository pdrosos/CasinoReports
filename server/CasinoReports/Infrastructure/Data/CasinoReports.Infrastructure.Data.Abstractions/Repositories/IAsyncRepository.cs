namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Specifications.Query;

    public interface IAsyncRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(params object[] id);

        Task<TEntity> GetSingleBySpecAsync(IQuerySpecification<TEntity> spec);

        Task<IReadOnlyList<TEntity>> AllAsync();

        Task<IReadOnlyList<TEntity>> AllAsNoTrackingAsync();

        Task<IReadOnlyList<TEntity>> ListAsync(IQuerySpecification<TEntity> spec);

        Task<IReadOnlyList<TEntity>> ListAsNoTrackingAsync(IQuerySpecification<TEntity> spec);

        Task<int> CountAsync(IQuerySpecification<TEntity> spec);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
