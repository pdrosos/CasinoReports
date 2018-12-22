namespace CasinoReports.Core.Services.Csv
{
    using CasinoReports.Core.Models.Entities;
    using CsvHelper.Configuration;

    public class CustomerVisitsMap : ClassMap<CustomerVisits>
    {
        private readonly string[] trueValues = { "yes" };

        private readonly string[] falseValues = { "no" };

        private readonly string[] nullValues = { "na", "NA", "n/a", "N/A" };

        public CustomerVisitsMap()
        {
            this.AutoMap();

            this.Map(m => m.NameFirstLast)
                .Name("NameFirstLast", "Name_First_Last", "NameFirsLast", "Name_Firs_Last")
                .Index(1);

            this.Map(m => m.AvgBet)
                .TypeConverterOption
                .NullValues(this.nullValues);

            this.Map(m => m.BonusPercentOfBet)
                .TypeConverterOption
                .NullValues(this.nullValues);

            this.Map(m => m.PlayPercent)
                .TypeConverterOption
                .BooleanValues(true, true, this.trueValues)
                .TypeConverterOption
                .BooleanValues(false, true, this.falseValues)
                .TypeConverterOption
                .NullValues(this.nullValues);

            this.Map(m => m.NewCustomers)
                .TypeConverterOption
                .BooleanValues(true, true, this.trueValues)
                .TypeConverterOption
                .BooleanValues(false, true, this.falseValues)
                .TypeConverterOption
                .NullValues(this.nullValues);

            this.Map(m => m.HoldOnSept)
                .TypeConverterOption
                .BooleanValues(true, true, this.trueValues)
                .TypeConverterOption
                .BooleanValues(false, true, this.falseValues)
                .TypeConverterOption
                .NullValues(this.nullValues);

            this.Map(m => m.HoldOnOkt)
                .TypeConverterOption
                .BooleanValues(true, true, this.trueValues)
                .TypeConverterOption
                .BooleanValues(false, true, this.falseValues)
                .TypeConverterOption
                .NullValues(this.nullValues);
        }
    }
}
