namespace CasinoReports.Infrastructure.Data
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    internal class EntityPropertiesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                if (property.Relational().ColumnType == null)
                {
                    property.Relational().ColumnType = "decimal(22, 10)";
                }
            }
        }
    }
}
