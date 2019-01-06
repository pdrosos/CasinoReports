namespace CasinoReports.Web.Api.Models.CustomerVisitsReport
{
    using System;

    using Newtonsoft.Json.Linq;

    public class CustomerVisitsReportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public JObject Settings { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
