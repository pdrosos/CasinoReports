namespace CasinoReports.Core.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Casino : BaseEquatableDeletableEntity<int, Casino>
    {
        public Casino(string name)
            : this()
        {
            this.Name = name;
        }

        private Casino()
        {
            // used by EF Core
            this.CasinoManagers = new HashSet<CasinoManager>();
            this.CustomerVisitsCollectionCasinos = new HashSet<CustomerVisitsCollectionCasino>();
        }

        public string Name { get; private set; }

        public ICollection<CasinoManager> CasinoManagers { get; }

        public ICollection<CustomerVisitsCollectionCasino> CustomerVisitsCollectionCasinos { get; }

        public void AddCasinoManager(CasinoManager casinoManager)
        {
            if (this.CasinoManagers.Contains(casinoManager))
            {
                return;
            }

            this.CasinoManagers.Add(casinoManager);
        }

        public bool RemoveCasinoManager(CasinoManager casinoManager)
        {
            return this.CasinoManagers.Remove(casinoManager);
        }
    }
}
