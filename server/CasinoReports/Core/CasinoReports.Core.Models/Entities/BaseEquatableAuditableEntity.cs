namespace CasinoReports.Core.Models.Entities
{
    using System;

    public abstract class BaseEquatableAuditableEntity<TKey, TEntity> : BaseAuditableEntity, IEquatable<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseEquatableAuditableEntity<TKey, TEntity>
    {
        private int? hashCode;

        public TKey Id { get; private set; }

        public static bool operator ==(
            BaseEquatableAuditableEntity<TKey, TEntity> first,
            BaseEquatableAuditableEntity<TKey, TEntity> second)
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

        public static bool operator !=(
            BaseEquatableAuditableEntity<TKey, TEntity> first,
            BaseEquatableAuditableEntity<TKey, TEntity> second)
        {
            return !(first == second);
        }

        public virtual bool Equals(TEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            bool otherIsTransient = Equals(other.Id, default(TKey));
            bool thisIsTransient = Equals(this.Id, default(TKey));
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

            var item = (TEntity)other;

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
            bool thisIsTransient = Equals(this.Id, default(TKey));
            if (thisIsTransient)
            {
                this.hashCode = base.GetHashCode();

                return this.hashCode.Value;
            }

            return this.Id.GetHashCode();
        }
    }
}
