namespace CasinoReports.Core.Models.Entities
{
    using System;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity, IDeletableEntity, IEquatable<ApplicationRole>
    {
        public const string Administrator = "Administrator";

        public const string ChiefManager = "ChiefManager";

        public const string CasinoManager = "CasinoManager";

        private int? hashCode;

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid();
        }

        private ApplicationRole()
        {
            // used by EF Core
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public static bool operator ==(ApplicationRole first, ApplicationRole second)
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

        public static bool operator !=(ApplicationRole first, ApplicationRole second)
        {
            return !(first == second);
        }

        public bool Equals(ApplicationRole other)
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

            var item = (ApplicationRole)other;

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
