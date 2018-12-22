namespace CasinoReports.Core.Models.Entities
{
    using System;

    public class CustomerVisitsCollectionCasino : BaseEquatableDeletableEntity<int, CustomerVisitsCollectionCasino>
    {
        public CustomerVisitsCollectionCasino(
            CustomerVisitsCollection customerVisitsCollection,
            Casino casino)
        {
            if (customerVisitsCollection == null)
            {
                throw new ArgumentNullException(nameof(customerVisitsCollection));
            }

            if (casino == null)
            {
                throw new ArgumentNullException(nameof(casino));
            }

            this.CustomerVisitsCollectionId = customerVisitsCollection.Id;
            this.CustomerVisitsCollection = customerVisitsCollection;

            this.CasinoId = casino.Id;
            this.Casino = casino;
        }

        private CustomerVisitsCollectionCasino()
        {
            // used by EF Core
        }

        public int CustomerVisitsCollectionId { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }

        public int CasinoId { get; private set; }

        public Casino Casino { get; private set; }

        public override bool Equals(CustomerVisitsCollectionCasino other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return this.CustomerVisitsCollectionId.Equals(other.CustomerVisitsCollectionId) &&
                   this.CasinoId.Equals(other.CasinoId);
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
                           (!ReferenceEquals(null, this.CasinoId) ? this.CasinoId.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}
