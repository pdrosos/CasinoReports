namespace CasinoReports.Core.Models.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class CustomerVisitsReport : BaseEquatableDeletableEntity<int, CustomerVisitsReport>
    {
        public CustomerVisitsReport(string name, CustomerVisitsCollection customerVisitsCollection)
        {
            this.Name = name;
            this.CustomerVisitsCollection = customerVisitsCollection;
        }

        private CustomerVisitsReport()
        {
            // used by EF Core
        }

        public string Name { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }
    }
}
