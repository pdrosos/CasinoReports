namespace CasinoReports.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Web.Api.Models.CustomerVisitsCollection;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Policy = AuthorizationPolicies.ManageCustomerVisitsData)]
    public class CustomerVisitsCollectionController : BaseController
    {
        private readonly ICustomerVisitsCollectionService customerVisitsCollectionService;

        public CustomerVisitsCollectionController(ICustomerVisitsCollectionService customerVisitsCollectionService)
        {
            this.customerVisitsCollectionService = customerVisitsCollectionService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CustomerVisitsCollectionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            IReadOnlyList<CustomerVisitsCollection> customerVisitsCollections =
                await this.customerVisitsCollectionService.GetAllAsNoTrackingAsync();

            // todo: probably use Automapper
            IEnumerable<CustomerVisitsCollectionViewModel> viewModel = customerVisitsCollections.Select(c =>
                new CustomerVisitsCollectionViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedOn = c.CreatedOn.ToLocalTime(),
                });

            return this.Ok(viewModel);
        }
    }
}
