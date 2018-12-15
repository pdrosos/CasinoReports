namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;

    public class CustomerVisitsImport : BaseEquatableDeletableEntity<int, CustomerVisitsImport>
    {
        public CustomerVisitsImport(string name, CustomerVisitsCollection customerVisitsCollection)
        {
            this.Name = name;
            this.CustomerVisitsCollection = customerVisitsCollection;
            this.CustomerVisits = new List<CustomerVisits>();
        }

        private CustomerVisitsImport()
        {
            // used by EF Core
        }

        public string Name { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }

        public ICollection<CustomerVisits> CustomerVisits { get; }
    }
}
