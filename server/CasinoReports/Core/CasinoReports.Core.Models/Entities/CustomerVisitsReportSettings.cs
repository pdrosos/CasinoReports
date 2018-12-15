namespace CasinoReports.Core.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class CustomerVisitsReportSettings
    {
        public CustomerVisitsReportSettings()
        {
            this.Rows = new List<string>();
            this.Columns = new List<string>();
            this.Values = new List<string>();
        }

        public IReadOnlyList<string> Rows { get; }

        public IReadOnlyList<string> Columns { get; }

        public IReadOnlyList<string> Values { get; }
    }
}
