namespace CasinoReports.Core.Specifications
{
    using System;
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        Expression<Func<T, bool>> ToExpression();
    }
}
