namespace CasinoReports.Core.Models.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity, IDeletableEntity, IEquatable<ApplicationUser>
    {
        private int? hashCode;

        public ApplicationUser(string userName)
            : base(userName)
        {
            this.Id = Guid.NewGuid();

            this.IdentityUserRoles = new HashSet<IdentityUserRole<Guid>>();
            this.IdentityUserClaims = new HashSet<IdentityUserClaim<Guid>>();
            this.IdentityUserLogins = new HashSet<IdentityUserLogin<Guid>>();

            this.CasinoManagers = new HashSet<CasinoManager>();
            this.CustomerVisitsCollectionUsers = new HashSet<CustomerVisitsCollectionUser>();
        }

        private ApplicationUser()
        {
            // used by EF Core
            this.IdentityUserRoles = new HashSet<IdentityUserRole<Guid>>();
            this.IdentityUserClaims = new HashSet<IdentityUserClaim<Guid>>();
            this.IdentityUserLogins = new HashSet<IdentityUserLogin<Guid>>();

            this.CasinoManagers = new HashSet<CasinoManager>();
            this.CustomerVisitsCollectionUsers = new HashSet<CustomerVisitsCollectionUser>();
        }

        public ICollection<IdentityUserRole<Guid>> IdentityUserRoles { get; }

        public ICollection<IdentityUserClaim<Guid>> IdentityUserClaims { get; }

        public ICollection<IdentityUserLogin<Guid>> IdentityUserLogins { get; }

        public ICollection<CasinoManager> CasinoManagers { get; }

        public ICollection<CustomerVisitsCollectionUser> CustomerVisitsCollectionUsers { get; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public static bool operator ==(ApplicationUser first, ApplicationUser second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null))
            {
                return false;
            }

            if (ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Equals(second);
        }

        public static bool operator !=(ApplicationUser first, ApplicationUser second)
        {
            return !(first == second);
        }

        public void AddIdentityUserRole(IdentityUserRole<Guid> identityUserRole)
        {
            if (this.IdentityUserRoles.Contains(identityUserRole))
            {
                return;
            }

            this.IdentityUserRoles.Add(identityUserRole);
        }

        public bool RemoveIdentityUserRole(IdentityUserRole<Guid> identityUserRole)
        {
            return this.IdentityUserRoles.Remove(identityUserRole);
        }

        public bool Equals(ApplicationUser other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            bool otherIsTransient = Equals(other.Id, default(Guid));
            bool thisIsTransient = Equals(this.Id, default(Guid));
            if (otherIsTransient && thisIsTransient)
            {
                return ReferenceEquals(this, other);
            }

            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            var item = (ApplicationUser)other;

            return this.Equals(item);
        }

        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (this.hashCode.HasValue)
            {
                return this.hashCode.Value;
            }

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can never change its hash code.
            bool thisIsTransient = Equals(this.Id, default(Guid));
            if (thisIsTransient)
            {
                this.hashCode = base.GetHashCode();

                return this.hashCode.Value;
            }

            return this.Id.GetHashCode();
        }
    }
}
