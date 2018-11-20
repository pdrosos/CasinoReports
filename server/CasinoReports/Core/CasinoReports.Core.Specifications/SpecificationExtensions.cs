namespace CasinoReports.Core.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            var all = new IdentitySpecification<T>();

            if (left == all)
            {
                return right;
            }

            if (right == all)
            {
                return left;
            }

            return new AndSpecification<T>(left, right);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            var all = new IdentitySpecification<T>();

            if (left == all || right == all)
            {
                return all;
            }

            return new OrSpecification<T>(left, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }
}
