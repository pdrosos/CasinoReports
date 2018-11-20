namespace CasinoReports.Core.Specifications
{
    using System;
    using System.Linq.Expressions;

    using LinqKit;

    internal sealed class AndSpecification<T> : BaseSpecification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.right = right;
            this.left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return this.left.ToExpression().And(this.right.ToExpression());
        }
    }
}
