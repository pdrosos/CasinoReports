﻿namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;

    public class CustomerVisitsImport : BaseEquatableDeletableEntity<int, CustomerVisitsImport>
    {
        public CustomerVisitsImport(string name, CustomerVisitsCollection customerVisitsCollection)
            : this()
        {
            this.Name = name;
            this.CustomerVisitsCollection = customerVisitsCollection;
        }

        private CustomerVisitsImport()
        {
            // used by EF Core
            this.CustomerVisits = new HashSet<CustomerVisits>();
        }

        public string Name { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }

        public ICollection<CustomerVisits> CustomerVisits { get; }

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