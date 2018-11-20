namespace CasinoReports.Core.Specifications
{
    using System;
    using System.Linq.Expressions;

    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public static readonly BaseSpecification<T> All = new IdentitySpecification<T>();

        private Func<T, bool> predicate;

        public bool IsSatisfiedBy(T entity)
        {
            this.predicate = this.predicate ?? this.ToExpression().Compile();

            return this.predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
