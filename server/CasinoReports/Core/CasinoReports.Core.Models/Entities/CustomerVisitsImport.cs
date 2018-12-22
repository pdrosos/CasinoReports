namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;

    public class CustomerVisitsImport : BaseEquatableDeletableEntity<int, CustomerVisitsImport>
    {
        public CustomerVisitsImport(string name)
            : this()
        {
            this.Name = name;
        }

        private CustomerVisitsImport()
        {
            // used by EF Core
            this.CustomerVisitsCollectionImports = new HashSet<CustomerVisitsCollectionImport>();
            this.CustomerVisits = new HashSet<CustomerVisits>();
        }

        public string Name { get; private set; }

        public ICollection<CustomerVisitsCollectionImport> CustomerVisitsCollectionImports { get; }

        public ICollection<CustomerVisits> CustomerVisits { get; }

        public void AddCustomerVisitsCollectionImport(CustomerVisitsCollectionImport customerVisitsCollectionImport)
        {
            if (this.CustomerVisitsCollectionImports.Contains(customerVisitsCollectionImport))
            {
                return;
            }

            this.CustomerVisitsCollectionImports.Add(customerVisitsCollectionImport);
        }

        public bool RemoveCustomerVisitsCollectionImport(CustomerVisitsCollectionImport customerVisitsCollectionImport)
        {
            return this.CustomerVisitsCollectionImports.Remove(customerVisitsCollectionImport);
        }

        public void AddCustomerVisits(CustomerVisits customerVisits)
        {
            if (this.CustomerVisits.Contains(customerVisits))
            {
                return;
            }

            this.CustomerVisits.Add(customerVisits);
        }

        public bool RemoveCustomerVisits(CustomerVisits customerVisits)
        {
            return this.CustomerVisits.Remove(customerVisits);
        }
    }
}
