namespace CasinoReports.Core.Specifications.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class BaseQuerySpecification<T> : IQuerySpecification<T>
    {
        protected BaseQuerySpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public IList<string> IncludeStrings { get; } = new List<string>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        public int Skip { get; private set; }

        public int Take { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            this.IncludeStrings.Add(includeString);
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            this.IsPagingEnabled = true;
            this.Skip = skip;
            this.Take = take;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            this.OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            this.OrderByDescending = orderByDescendingExpression;
        }
    }
}
