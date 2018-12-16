namespace CasinoReports.Core.Models.Entities
{
    using System;

    public class CustomerVisits : BaseEquatableDeletableEntity<long, CustomerVisits>
    {
        public CustomerVisits(CustomerVisitsImport customerVisitsImport)
        {
            this.CustomerVisitsImport = customerVisitsImport;
        }

        public CustomerVisits()
        {
            // used by CsvHelper and by EF Core
        }

        public CustomerVisitsImport CustomerVisitsImport { get; set; }

        public string NameFirstLast { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime Date { get; set; }

        public string PreferGame { get; set; }

        public int Visits { get; set; }

        public decimal? AvgBet { get; set; }

        public string PlayerType { get; set; }

        public decimal TotalBet { get; set; }

        public decimal Balance { get; set; }

        public int Bonus { get; set; }

        public int BonusFromPoints { get; set; }

        public int MatchPay { get; set; }

        public int TombolaGame { get; set; }

        public decimal TotalBonuses { get; set; }

        public decimal CleanBalance { get; set; }

        public decimal? BonusPercentOfBet { get; set; }

        public decimal BonusPercentOfLose { get; set; }

        public bool? PlayPercent { get; set; }

        public bool? NewCustomers { get; set; }

        public bool? HoldOnSept { get; set; }

        public bool? HoldOnOkt { get; set; }

        public bool Holded { get; set; }

        public string TotalBetRange { get; set; }
    }
}
