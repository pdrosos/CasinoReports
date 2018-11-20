namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        TEntity GetByIdWithDeleted(object[] id);

        TEntity GetSingleBySpecWithDeleted(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> AllWithDeleted();

        IReadOnlyList<TEntity> AllWithDeletedAsNoTracking();

        IReadOnlyList<TEntity> ListWithDeleted(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> ListWithDeletedAsNoTracking(IQuerySpecification<TEntity> spec);

        void Undelete(TEntity entity);

        void HardDelete(TEntity entity);
    }
}
