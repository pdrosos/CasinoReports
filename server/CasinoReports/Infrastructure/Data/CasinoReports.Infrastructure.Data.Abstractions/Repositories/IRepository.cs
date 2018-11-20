namespace CasinoReports.Infrastructure.Data.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;

    using CasinoReports.Core.Specifications.Query;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity GetById(params object[] id);

        TEntity GetSingleBySpec(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> All();

        IReadOnlyList<TEntity> AllAsNoTracking();

        IReadOnlyList<TEntity> List(IQuerySpecification<TEntity> spec);

        IReadOnlyList<TEntity> ListAsNoTracking(IQuerySpecification<TEntity> spec);

        int Count(IQuerySpecification<TEntity> spec);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        int SaveChanges();
    }
}
