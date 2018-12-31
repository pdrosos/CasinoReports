namespace CasinoReports.Core.Services.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Dtos;
    using CasinoReports.Core.Models.Entities;

    public interface ICustomerVisitsImportService
    {
        Task<IReadOnlyList<CustomerVisitsImport>> GetAllWithCollectionsAsNoTrackingAsync();

        Task<int> CreateAsync(
            string name,
            IEnumerable<int> customerVisitsCollectionIds,
            IEnumerable<CustomerVisitsDto> customerVisitsDtos);
    }
}
