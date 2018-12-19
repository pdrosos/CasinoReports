namespace CasinoReports.Core.Services.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    public interface ICustomerVisitsImportService
    {
        Task<IReadOnlyList<CustomerVisitsImport>> GetAllWithCollectionAsNoTrackingAsync();
    }
}
