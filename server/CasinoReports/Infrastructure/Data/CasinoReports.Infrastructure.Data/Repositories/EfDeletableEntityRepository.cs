namespace CasinoReports.Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>,
        IDeletableEntityRepository<TEntity>, IDeletableEntityAsyncRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        private readonly IQueryable<TEntity> allWithDeletedQueryable;

        public EfDeletableEntityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this.allWithDeletedQueryable = this.DbSet.IgnoreQueryFilters();
        }

        public override TEntity GetById(params object[] id)
        {
            var entity = base.GetById(id);
            if (entity?.IsDeleted ?? false)
            {
                return null;
            }

            return entity;
        }

        public override async Task<TEntity> GetByIdAsync(params object[] id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity?.IsDeleted ?? false)
            {
                return null;
            }

            return entity;
        }

        public TEntity GetByIdWithDeleted(object[] id)
        {
            return base.GetById(id);
        }

        public async Task<TEntity> GetByIdWithDeletedAsync(object[] id)
        {
            return await base.GetByIdAsync(id);
        }

        public TEntity GetSingleBySpecWithDeleted(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).FirstOrDefault();
        }

        public async Task<TEntity> GetSingleBySpecWithDeletedAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public IReadOnlyList<TEntity> AllWithDeleted()
        {
            return this.allWithDeletedQueryable.ToList();
        }

        public async Task<IReadOnlyList<TEntity>> AllWithDeletedAsync()
        {
            return await this.allWithDeletedQueryable.ToListAsync();
        }

        public IReadOnlyList<TEntity> AllWithDeletedAsNoTracking()
        {
            return this.allWithDeletedQueryable.AsNoTracking().ToList();
        }

        public async Task<IReadOnlyList<TEntity>> AllWithDeletedAsNoTrackingAsync()
        {
            return await this.allWithDeletedQueryable.AsNoTracking().ToListAsync();
        }

        public IReadOnlyList<TEntity> ListWithDeleted(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).ToList();
        }

        public async Task<IReadOnlyList<TEntity>> ListWithDeletedAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        public IReadOnlyList<TEntity> ListWithDeletedAsNoTracking(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).AsNoTracking().ToList();
        }

        public async Task<IReadOnlyList<TEntity>> ListWithDeletedAsNoTrackingAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            this.Update(entity);
        }

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;

            this.Update(entity);
        }

        public void HardDelete(TEntity entity)
        {
            base.Delete(entity);
        }

        private IQueryable<TEntity> ApplySpecification(IQuerySpecification<TEntity> spec)
        {
            return QuerySpecificationEvaluator<TEntity>.GetQuery(this.allWithDeletedQueryable, spec);
        }
    }
}
