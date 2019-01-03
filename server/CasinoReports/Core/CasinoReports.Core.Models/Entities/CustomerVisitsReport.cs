namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class CustomerVisitsReport : BaseEquatableDeletableEntity<int, CustomerVisitsReport>
    {
        public CustomerVisitsReport(string name)
            : this()
        {
            this.Name = name;
        }

        private CustomerVisitsReport()
        {
            // used by EF Core
            this.Filters = new List<string>();
            this.Rows = new List<string>();
            this.Columns = new List<string>();
            this.Values = new List<string>();
        }

        public string Name { get; private set; }

        public IReadOnlyList<string> Filters { get; }

        public IReadOnlyList<string> Rows { get; }

        public IReadOnlyList<string> Columns { get; }

        public IReadOnlyList<string> Values { get; }
    }
}
