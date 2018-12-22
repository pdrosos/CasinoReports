namespace CasinoReports.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Core.Services.Csv;
    using CasinoReports.Web.Api.Models.CustomerVisitsCollection;
    using CasinoReports.Web.Api.Models.CustomerVisitsImport;

    using CsvHelper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize(Roles = ApplicationRole.Administrator)]
    [Authorize(Policy = AuthorizationPolicies.ManageCustomerVisitsData)]
    public class CustomerVisitsImportController : BaseController
    {
        private readonly ICustomerVisitsImportService customerVisitsImportService;

        public CustomerVisitsImportController(
            ICustomerVisitsImportService customerVisitsImportService)
        {
            this.customerVisitsImportService = customerVisitsImportService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CustomerVisitsImportViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            IReadOnlyList<CustomerVisitsImport> customerVisitsImports =
                await this.customerVisitsImportService.GetAllWithCollectionsAsNoTrackingAsync();

            // todo: probably use Automapper
            IEnumerable<CustomerVisitsImportViewModel> viewModel =
                customerVisitsImports.Select(c => new CustomerVisitsImportViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Collections = c.CustomerVisitsCollectionImports.Select(ci => new CustomerVisitsCollectionViewModel
                    {
                        Id = ci.CustomerVisitsCollection.Id,
                        Name = ci.CustomerVisitsCollection.Name,
                        CreatedOn = ci.CustomerVisitsCollection.CreatedOn,
                    }),
                    CreatedOn = c.CreatedOn.ToLocalTime(),
                });

            return this.Ok(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromForm] CustomerVisitsImportInputModel customerVisitsImportInputModel)
        {
            try
            {
                IFormFile file = customerVisitsImportInputModel.CustomerVisits;
                using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("windows-1251")))
                {
                    using (CsvReader csvReader = CustomerVisitsCsvFactory.CreateReader(streamReader))
                    {
                        IEnumerable<CustomerVisits> customerVisits = csvReader.GetRecords<CustomerVisits>();

                        await this.customerVisitsImportService.CreateAsync(
                            customerVisitsImportInputModel.Name,
                            customerVisitsImportInputModel.CustomerVisitsCollectionIds,
                            customerVisits);
                    }
                }
            }
            catch (Exception e)
            {
                return this.UnprocessableEntity(e);
            }

            return this.Ok();
        }
    }
}
