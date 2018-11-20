namespace CasinoReports.Core.Specifications
{
    using System;
    using System.Linq.Expressions;

    internal sealed class IdentitySpecification<T> : BaseSpecification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}
