namespace CasinoReports.Core.Models.Entities
{
    using System;

    public abstract class BaseAuditableEntity : IAuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
