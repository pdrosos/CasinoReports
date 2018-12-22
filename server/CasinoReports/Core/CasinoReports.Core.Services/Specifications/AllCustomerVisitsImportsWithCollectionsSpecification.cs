namespace CasinoReports.Core.Services.Specifications
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public class AllCustomerVisitsImportsWithCollectionsSpecification : BaseQuerySpecification<CustomerVisitsImport>
    {
        public AllCustomerVisitsImportsWithCollectionsSpecification()
            : base(c => true)
        {
            this.AddInclude(c => c.CustomerVisitsCollectionImports);
            this.AddInclude($"{nameof(CustomerVisitsImport.CustomerVisitsCollectionImports)}.{nameof(CustomerVisitsCollectionImport.CustomerVisitsCollection)}");
            this.ApplyOrderBy(c => c.Id);
        }
    }
}
