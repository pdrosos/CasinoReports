namespace CasinoReports.Core.Models.Entities
{
    using System;

    public interface IAuditableEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
