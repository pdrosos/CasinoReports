namespace CasinoReports.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
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
        private readonly ICustomerVisitsImportRepository customerVisitsImportRepository;

        private readonly ICustomerVisitsCollectionService customerVisitsCollectionService;

        private readonly ICustomerVisitsImportService customerVisitsImportService;

        public CustomerVisitsImportController(
            ICustomerVisitsImportRepository customerVisitsImportRepository,
            ICustomerVisitsCollectionService customerVisitsCollectionService,
            ICustomerVisitsImportService customerVisitsImportService)
        {
            this.customerVisitsImportRepository = customerVisitsImportRepository;
            this.customerVisitsCollectionService = customerVisitsCollectionService;
            this.customerVisitsImportService = customerVisitsImportService;
        }

        public async Task<IActionResult> Get()
        {
            IReadOnlyList<CustomerVisitsImport> customerVisitsImports =
                await this.customerVisitsImportService.GetAllWithCollectionAsNoTrackingAsync();

            // todo: probably use Automapper
            IEnumerable<CustomerVisitsImportViewModel> viewModel =
                customerVisitsImports.Select(c => new CustomerVisitsImportViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Collection = c.CustomerVisitsCollection.Name,
                    CreatedOn = c.CreatedOn.ToLocalTime(),
                });

            return this.Ok(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromForm] CustomerVisitsImportInputModel customerVisitsImportInputModel)
        {
            CustomerVisitsCollection customerVisitsCollection = await this.customerVisitsCollectionService.GetByIdAsync(
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
