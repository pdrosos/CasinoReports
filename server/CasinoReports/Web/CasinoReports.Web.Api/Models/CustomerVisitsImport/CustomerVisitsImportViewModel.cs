namespace CasinoReports.Web.Api.Models.CustomerVisitsImport
{
    using System;

    public class CustomerVisitsImportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CollectionId { get; set; }

        public string Collection { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
