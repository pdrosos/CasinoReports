namespace CasinoReports.Web.FlexmonsterApi
{
    using Flexmonster.Accelerator.Controllers;
    using Flexmonster.Accelerator.Utils;

    public class FlexmonsterConfig
    {
        public static void Register()
        {
            // Replace with actual data source.
            // Example: Data Source=localhost
            FlexmonsterProxyController.ConnectionString = "Data Source=.\\MSSQLSERVER_TAB";
            CacheManager.Enabled = true;
            CacheManager.MemoryLimit = 10 * 1024 * 1024; // MB to bytes
        }
    }
}
