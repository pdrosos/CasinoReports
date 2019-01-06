namespace CasinoReports.Core.Models.Entities
{
    using Newtonsoft.Json.Linq;

    public class CustomerVisitsReport : BaseEquatableDeletableEntity<int, CustomerVisitsReport>
    {
        public CustomerVisitsReport(string name, JObject settings)
        {
            this.Name = name;
            this.Settings = settings;
        }

        private CustomerVisitsReport()
        {
            // used by EF Core
        }

        public string Name { get; set; }

        public JObject Settings { get; set; }
    }
}
