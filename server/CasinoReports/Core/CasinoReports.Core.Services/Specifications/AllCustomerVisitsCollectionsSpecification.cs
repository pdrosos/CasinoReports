namespace CasinoReports.Core.Services.Specifications
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public class AllCustomerVisitsCollectionsSpecification : BaseQuerySpecification<CustomerVisitsCollection>
    {
        public AllCustomerVisitsCollectionsSpecification()
            : base(c => true)
        {
            this.ApplyOrderBy(c => c.Id);
        }
    }
}
