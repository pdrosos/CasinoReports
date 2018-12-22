namespace CasinoReports.Core.Services.Csv
{
    using System.IO;

    using CsvHelper;

    public class CustomerVisitsCsvFactory
    {
        public static CsvReader CreateReader(StreamReader streamReader)
        {
            var csvReader = new CsvReader(streamReader);

            csvReader.Configuration.RegisterClassMap<CustomerVisitsMap>();
            csvReader.Configuration.ShouldUseConstructorParameters = type => false;
            csvReader.Configuration.MissingFieldFound = null;

            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.PrepareHeaderForMatch = (header, index) =>
                header.Replace("_", string.Empty).ToLower();

            return csvReader;
        }
    }
}
