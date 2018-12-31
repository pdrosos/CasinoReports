namespace CasinoReports.Core.Models.Entities
{
    using System;

    using CasinoReports.Core.Models.Dtos;

    public class CustomerVisits : BaseEquatableDeletableEntity<long, CustomerVisits>
    {
        public CustomerVisits(CustomerVisitsImport customerVisitsImport)
        {
            this.CustomerVisitsImport = customerVisitsImport;
        }

        private CustomerVisits()
        {
            // used by EF Core
        }

        public CustomerVisitsImport CustomerVisitsImport { get; set; }

        public string NameFirstLast { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime Date { get; set; }

        public CasinoGame PreferGame { get; set; }

        public int Visits { get; set; }

        public decimal? AvgBet { get; set; }

        public CasinoPlayerType PlayerType { get; set; }

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

        public CustomerTotalBetRange TotalBetRange { get; set; }

        public static CustomerVisits FromDto(
            CustomerVisitsDto customerVisitsDto,
            CustomerVisitsImport customerVisitsImport,
            CasinoGame casinoGame,
            CasinoPlayerType casinoPlayerType,
            CustomerTotalBetRange customerTotalBetRange)
        {
            var customerVisits = new CustomerVisits(customerVisitsImport)
            {
                NameFirstLast = customerVisitsDto.NameFirstLast,
                BirthDate = customerVisitsDto.BirthDate,
                Date = customerVisitsDto.Date,
                PreferGame = casinoGame,
                Visits = customerVisitsDto.Visits,
                AvgBet = customerVisitsDto.AvgBet,
                PlayerType = casinoPlayerType,
                TotalBet = customerVisitsDto.TotalBet,
                Balance = customerVisitsDto.Balance,
                Bonus = customerVisitsDto.Bonus,
                BonusFromPoints = customerVisitsDto.BonusFromPoints,
                MatchPay = customerVisitsDto.MatchPay,
                TombolaGame = customerVisitsDto.TombolaGame,
                TotalBonuses = customerVisitsDto.TotalBonuses,
                CleanBalance = customerVisitsDto.CleanBalance,
                BonusPercentOfBet = customerVisitsDto.BonusPercentOfBet,
                BonusPercentOfLose = customerVisitsDto.BonusPercentOfLose,
                PlayPercent = customerVisitsDto.PlayPercent,
                NewCustomers = customerVisitsDto.NewCustomers,
                HoldOnSept = customerVisitsDto.HoldOnSept,
                HoldOnOkt = customerVisitsDto.HoldOnOkt,
                Holded = customerVisitsDto.Holded,
                TotalBetRange = customerTotalBetRange,
            };

            return customerVisits;
        }
    }
}
