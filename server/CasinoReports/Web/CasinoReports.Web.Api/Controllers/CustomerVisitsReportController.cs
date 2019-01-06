namespace CasinoReports.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;
    using CasinoReports.Web.Api.Models.CustomerVisitsReport;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize(Roles = ApplicationRole.Administrator)]
    [Authorize(Policy = AuthorizationPolicies.ManageReports)]
    public class CustomerVisitsReportController : BaseController
    {
        private readonly ICustomerVisitsReportRepository customerVisitsReportRepository;
        private readonly ICustomerVisitsReportService customerVisitsReportService;

        public CustomerVisitsReportController(
            ICustomerVisitsReportService customerVisitsReportService,
            ICustomerVisitsReportRepository customerVisitsReportRepository)
        {
            this.customerVisitsReportRepository = customerVisitsReportRepository;
            this.customerVisitsReportService = customerVisitsReportService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CustomerVisitsReportViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            IReadOnlyList<CustomerVisitsReport> customerVisitsReports =
                await this.customerVisitsReportService.GetAllAsNoTrackingAsync();

            // todo: probably use Automapper
            IEnumerable<CustomerVisitsReportViewModel> viewModel =
                customerVisitsReports.Select(r => new CustomerVisitsReportViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Settings = r.Settings,
                    CreatedOn = r.CreatedOn.ToLocalTime(),
                });

            return this.Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var customerVisitsReport = await this.customerVisitsReportRepository.GetByIdAsync(id);
            if (customerVisitsReport == null)
            {
                return this.NotFound(nameof(id));
            }

            var viewModel = new CustomerVisitsReportViewModel
            {
                Id = customerVisitsReport.Id,
                Name = customerVisitsReport.Name,
                Settings = customerVisitsReport.Settings,
                CreatedOn = customerVisitsReport.CreatedOn.ToLocalTime(),
            };

            return this.Ok(viewModel);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody]CustomerVisitsReportInputModel customerVisitsReportInputModel)
        {
            var customerVisitsReport = new CustomerVisitsReport(
                customerVisitsReportInputModel.Name,
                customerVisitsReportInputModel.Settings);

            this.customerVisitsReportRepository.Add(customerVisitsReport);
            await this.customerVisitsReportRepository.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody]CustomerVisitsReportInputModel customerVisitsReportInputModel)
        {
            var customerVisitsReport = await this.customerVisitsReportRepository.GetByIdAsync(id);
            if (customerVisitsReport == null)
            {
                return this.NotFound(nameof(id));
            }

            customerVisitsReport.Name = customerVisitsReportInputModel.Name;
            customerVisitsReport.Settings = customerVisitsReportInputModel.Settings;

            this.customerVisitsReportRepository.Update(customerVisitsReport);
            await this.customerVisitsReportRepository.SaveChangesAsync();

            return this.Ok();
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var customerVisitsReport = await this.customerVisitsReportRepository.GetByIdAsync(id);
            if (customerVisitsReport == null)
            {
                return this.NotFound(nameof(id));
            }

            this.customerVisitsReportRepository.Delete(customerVisitsReport);
            await this.customerVisitsReportRepository.SaveChangesAsync();

            return this.Ok();
        }
    }
}
