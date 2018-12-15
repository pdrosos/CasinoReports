namespace CasinoReports.Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasinoReports.Core.Specifications.Query;
    using CasinoReports.Infrastructure.Data;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class EfRepository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity>
        where TEntity : class
    {
        public EfRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        protected ApplicationDbContext DbContext { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public virtual TEntity GetById(params object[] id)
        {
            return this.DbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await this.DbSet.FindAsync(id);
        }

        public virtual TEntity GetSingleBySpec(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetSingleBySpecAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public virtual IReadOnlyList<TEntity> All()
        {
            return this.DbSet.ToList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> AllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual IReadOnlyList<TEntity> AllAsNoTracking()
        {
            return this.DbSet.AsNoTracking().ToList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> AllAsNoTrackingAsync()
        {
            return await this.DbSet.AsNoTracking().ToListAsync();
        }

        public virtual IReadOnlyList<TEntity> List(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).ToList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        public virtual IReadOnlyList<TEntity> ListAsNoTracking(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).AsNoTracking().ToList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAsNoTrackingAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public virtual int Count(IQuerySpecification<TEntity> spec)
        {
            return this.ApplySpecification(spec).Count();
        }

        public virtual async Task<int> CountAsync(IQuerySpecification<TEntity> spec)
        {
            return await this.ApplySpecification(spec).CountAsync();
        }

        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            this.DbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> ApplySpecification(IQuerySpecification<TEntity> spec)
        {
            return QuerySpecificationEvaluator<TEntity>.GetQuery(this.DbSet, spec);
        }
    }
}
