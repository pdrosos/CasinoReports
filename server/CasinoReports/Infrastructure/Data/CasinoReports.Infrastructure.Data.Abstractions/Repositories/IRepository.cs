namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using CasinoReports.Core.Specifications.Query;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity GetById(params object[] id);

        TEntity GetSingleBySpec(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> All();

        IReadOnlyList<TEntity> AllAsNoTracking();

        ISet<TEntity> AllAsSet();

        ISet<TEntity> AllAsSetAsNoTracking();

        IImmutableSet<TEntity> AllAsImmutableSet();

        IImmutableSet<TEntity> AllAsImmutableSetAsNoTracking();

        IReadOnlyList<TEntity> List(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> ListAsNoTracking(IQuerySpecification<TEntity> spec);

        int Count(IQuerySpecification<TEntity> spec);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        int SaveChanges();
    }
}
