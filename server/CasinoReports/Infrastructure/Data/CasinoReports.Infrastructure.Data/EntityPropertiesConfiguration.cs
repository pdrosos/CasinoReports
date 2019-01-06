namespace CasinoReports.Infrastructure.Data
{
    using System.Linq;

    using CasinoReports.Core.Models.Entities;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

            modelBuilder.Entity<CustomerVisitsReport>()
                .Property(r => r.Settings)
                .HasColumnType("ntext")
                .HasConversion(
                    s => JsonConvert.SerializeObject(s),
                    s => JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(s) ? "{}" : s));
        }
    }
}
