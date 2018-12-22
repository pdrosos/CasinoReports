namespace CasinoReports.Core.Models.Entities
{
    using System;

    public class CasinoManager : BaseEquatableDeletableEntity<int, CasinoManager>
    {
        public CasinoManager(Casino casino, ApplicationUser applicationUser)
        {
            if (casino == null)
            {
                throw new ArgumentNullException(nameof(casino));
            }

            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            this.CasinoId = casino.Id;
            this.Casino = casino;

            this.ApplicationUserId = applicationUser.Id;
            this.ApplicationUser = applicationUser;
        }

        private CasinoManager()
        {
            // used by EF Core
        }

        public int CasinoId { get; private set; }

        public Casino Casino { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public override bool Equals(CasinoManager other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return this.CasinoId.Equals(other.CasinoId) && this.ApplicationUserId.Equals(other.ApplicationUserId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;

                hashCode = (hashCode * 23) +
                           (!ReferenceEquals(null, this.CasinoId) ? this.CasinoId.GetHashCode() : 0);
                hashCode = (hashCode * 23) +
                           (!ReferenceEquals(null, this.ApplicationUserId) ? this.ApplicationUserId.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}
