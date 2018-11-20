namespace CasinoReports.Core.Models.Entities
{
    using System;

    public abstract class BaseDeletableEntity : BaseAuditableEntity, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
