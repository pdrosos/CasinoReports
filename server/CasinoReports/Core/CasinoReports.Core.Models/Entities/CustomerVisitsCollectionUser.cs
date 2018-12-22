namespace CasinoReports.Core.Models.Entities
{
    using System;

    public class CustomerVisitsCollectionUser : BaseEquatableDeletableEntity<int, CustomerVisitsCollectionUser>
    {
        public CustomerVisitsCollectionUser(
            CustomerVisitsCollection customerVisitsCollection,
            ApplicationUser applicationUser)
        {
            if (customerVisitsCollection == null)
            {
                throw new ArgumentNullException(nameof(customerVisitsCollection));
            }

            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            this.CustomerVisitsCollectionId = customerVisitsCollection.Id;
            this.CustomerVisitsCollection = customerVisitsCollection;

            this.ApplicationUserId = applicationUser.Id;
            this.ApplicationUser = applicationUser;
        }

        private CustomerVisitsCollectionUser()
        {
            // used by EF Core
        }

        public int CustomerVisitsCollectionId { get; private set; }

        public CustomerVisitsCollection CustomerVisitsCollection { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public override bool Equals(CustomerVisitsCollectionUser other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return this.CustomerVisitsCollectionId.Equals(other.CustomerVisitsCollectionId) &&
                   this.ApplicationUserId.Equals(other.ApplicationUserId);
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
                           (!ReferenceEquals(null, this.ApplicationUserId) ? this.ApplicationUserId.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}
