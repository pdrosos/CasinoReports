namespace CasinoReports.Core.Specifications.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IQuerySpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        IList<Expression<Func<T, object>>> Includes { get; }

        IList<string> IncludeStrings { get; }

        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

        bool IsPagingEnabled { get; }

        int Skip { get; }

        int Take { get; }
    }
}
