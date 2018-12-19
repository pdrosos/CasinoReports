namespace CasinoReports.Core.Services.Specifications
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public class AllCustomerVisitsImportsWithCollectionSpecification : BaseQuerySpecification<CustomerVisitsImport>
    {
        public AllCustomerVisitsImportsWithCollectionSpecification()
            : base(c => true)
        {
            this.AddInclude(c => c.CustomerVisitsCollection);
            this.ApplyOrderBy(c => c.Id);
        }
    }
}
