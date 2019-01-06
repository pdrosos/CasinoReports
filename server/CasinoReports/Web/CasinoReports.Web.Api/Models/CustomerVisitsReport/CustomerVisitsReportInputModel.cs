namespace CasinoReports.Web.Api.Models.CustomerVisitsReport
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json.Linq;

    public class CustomerVisitsReportInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public JObject Settings { get; set; }
    }
}
