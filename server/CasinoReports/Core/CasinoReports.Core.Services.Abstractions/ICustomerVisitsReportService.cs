namespace CasinoReports.Core.Services.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Entities;

    public interface ICustomerVisitsReportService
    {
        Task<IReadOnlyList<CustomerVisitsReport>> GetAllAsNoTrackingAsync();
    }
}
