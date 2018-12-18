namespace CasinoReports.Web.Api.Controllers
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;
    using CasinoReports.Web.Api.Mapping;
    using CasinoReports.Web.Api.Models.CustomerVisitsImport;
    using CsvHelper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize(Roles = ApplicationRole.Administrator)]
    [Authorize(Policy = AuthorizationPolicies.ManageCustomerVisitsData)]
    public class CustomerVisitsImportController : BaseController
    {
        private readonly ICustomerVisitsCollectionRepository customerVisitsCollectionRepository;

        private readonly ICustomerVisitsImportRepository customerVisitsImportRepository;

        public CustomerVisitsImportController(
            ICustomerVisitsCollectionRepository customerVisitsCollectionRepository,
            ICustomerVisitsImportRepository customerVisitsImportRepository)
        {
            this.customerVisitsCollectionRepository = customerVisitsCollectionRepository;
            this.customerVisitsImportRepository = customerVisitsImportRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromForm] CustomerVisitsImportInputModel customerVisitsImportInputModel)
        {
            CustomerVisitsCollection customerVisitsCollection = await this.customerVisitsCollectionRepository.GetByIdAsync(
                customerVisitsImportInputModel.CustomerVisitsCollectionId);

            CustomerVisitsImport customerVisitsImport = new CustomerVisitsImport(
                customerVisitsImportInputModel.Name,
                customerVisitsCollection);

            IFormFile file = customerVisitsImportInputModel.CustomerVisits;
            using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("windows-1251")))
            {
                using (CsvReader csvReader = this.CreateCsvReader(streamReader))
                {
                    var records = csvReader.GetRecords<CustomerVisits>();
                    foreach (var customerVisits in records)
                    {
                        customerVisitsImport.AddCustomerVisits(customerVisits);
                    }

                    this.customerVisitsImportRepository.Add(customerVisitsImport);
                    await this.customerVisitsImportRepository.SaveChangesAsync();
                }
            }

            return this.Ok();
        }

        private CsvReader CreateCsvReader(StreamReader streamReader)
        {
            var csvReader = new CsvReader(streamReader);

            csvReader.Configuration.RegisterClassMap<CustomerVisitsMap>();
            csvReader.Configuration.ShouldUseConstructorParameters = type => false;
            csvReader.Configuration.MissingFieldFound = null;

            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.PrepareHeaderForMatch = (header, index) =>
                header.Replace("_", string.Empty).ToLower();

            return csvReader;
        }
    }
}
