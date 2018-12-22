namespace CasinoReports.Web.Api.Models.CustomerVisitsImport
{
    using System;
    using System.Collections.Generic;

    using CasinoReports.Web.Api.Models.CustomerVisitsCollection;

    public class CustomerVisitsImportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CustomerVisitsCollectionViewModel> Collections { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
