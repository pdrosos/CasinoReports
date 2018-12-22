namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;

    public class CustomerVisitsCollection : BaseEquatableDeletableEntity<int, CustomerVisitsCollection>
    {
        public CustomerVisitsCollection(string name)
            : this()
        {
            this.Name = name;
        }

        private CustomerVisitsCollection()
        {
            // used by EF Core
            this.CustomerVisitsCollectionCasinos = new HashSet<CustomerVisitsCollectionCasino>();
            this.CustomerVisitsCollectionUsers = new HashSet<CustomerVisitsCollectionUser>();
            this.CustomerVisitsCollectionImports = new HashSet<CustomerVisitsCollectionImport>();
        }

        public string Name { get; private set; }

        public ICollection<CustomerVisitsCollectionCasino> CustomerVisitsCollectionCasinos { get; }

        public ICollection<CustomerVisitsCollectionUser> CustomerVisitsCollectionUsers { get; }

        public ICollection<CustomerVisitsCollectionImport> CustomerVisitsCollectionImports { get; }

        public void AddCustomerVisitsCollectionCasino(CustomerVisitsCollectionCasino customerVisitsCollectionCasino)
        {
            if (this.CustomerVisitsCollectionCasinos.Contains(customerVisitsCollectionCasino))
            {
                return;
            }

            this.CustomerVisitsCollectionCasinos.Add(customerVisitsCollectionCasino);
        }

        public bool RemoveCustomerVisitsCollectionCasino(CustomerVisitsCollectionCasino customerVisitsCollectionCasino)
        {
            return this.CustomerVisitsCollectionCasinos.Remove(customerVisitsCollectionCasino);
        }

        public void AddCustomerVisitsCollectionUser(CustomerVisitsCollectionUser customerVisitsCollectionUser)
        {
            if (this.CustomerVisitsCollectionUsers.Contains(customerVisitsCollectionUser))
            {
                return;
            }

            this.CustomerVisitsCollectionUsers.Add(customerVisitsCollectionUser);
        }

        public bool RemoveCustomerVisitsCollectionUser(CustomerVisitsCollectionUser customerVisitsCollectionUser)
        {
            return this.CustomerVisitsCollectionUsers.Remove(customerVisitsCollectionUser);
        }

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
    }
}
