namespace CasinoReports.Core.Models.Entities
{
    using System;

    public class CustomerVisitsCollectionImport : BaseEquatableDeletableEntity<int, CustomerVisitsCollectionImport>
    {
        public CustomerVisitsCollectionImport(
            CustomerVisitsCollection customerVisitsCollection,
            CustomerVisitsImport customerVisitsImport)
        {
            if (customerVisitsCollection == null)
            {
                throw new ArgumentNullException(nameof(customerVisitsCollection));
            }

            if (customerVisitsImport == null)
            {
                throw new ArgumentNullException(nameof(customerVisitsImport));
            }

            this.CustomerVisitsCollectionId = customerVisitsCollection.Id;
            this.CustomerVisitsCollection = customerVisitsCollection;

            this.CustomerVisitsImportId = customerVisitsImport.Id;
            this.CustomerVisitsImport = customerVisitsImport;
        }

        public CustomerVisitsCollectionImport(int customerVisitsCollectionId, CustomerVisitsImport customerVisitsImport)
        {
            if (customerVisitsImport == null)
            {
                throw new ArgumentNullException(nameof(customerVisitsImport));
            }

            this.CustomerVisitsCollectionId = customerVisitsCollectionId;

            this.CustomerVisitsImportId = customerVisitsImport.Id;
            this.CustomerVisitsImport = customerVisitsImport;
        }

        private CustomerVisitsCollectionImport()
        {
            // used by EF Core
        }

        public int CustomerVisitsCollectionId { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }

        public int CustomerVisitsImportId { get; private set; }

        public CustomerVisitsImport CustomerVisitsImport { get; private set; }

        public override bool Equals(CustomerVisitsCollectionImport other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return this.CustomerVisitsCollectionId.Equals(other.CustomerVisitsCollectionId) &&
                   this.CustomerVisitsImportId.Equals(other.CustomerVisitsImportId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;

                hashCode = (hashCode * 23) +
                           (!ReferenceEquals(null, this.CustomerVisitsCollectionId) ?
                               this.CustomerVisitsCollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 23) +
                           (!ReferenceEquals(null, this.CustomerVisitsImportId) ?
                               this.CustomerVisitsImportId.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}
