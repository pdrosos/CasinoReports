namespace CasinoReports.Core.Services.Specifications
{
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Specifications.Query;

    public class AllCustomerVisitsReportsSpecification : BaseQuerySpecification<CustomerVisitsReport>
    {
        public AllCustomerVisitsReportsSpecification()
            : base(r => true)
        {
            this.ApplyOrderBy(r => r.Id);
        }
    }
}
