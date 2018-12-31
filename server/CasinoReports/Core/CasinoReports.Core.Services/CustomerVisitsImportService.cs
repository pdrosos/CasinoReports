namespace CasinoReports.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasinoReports.Core.Models.Dtos;
    using CasinoReports.Core.Models.Entities;
    using CasinoReports.Core.Services.Abstractions;
    using CasinoReports.Core.Services.Specifications;
    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;

    public class CustomerVisitsImportService : ICustomerVisitsImportService
    {
        private readonly ICasinoGameRepository casinoGameRepository;

        private readonly ICasinoPlayerTypeRepository casinoPlayerTypeRepository;

        private readonly ICustomerTotalBetRangeRepository customerTotalBetRangeRepository;

        private readonly ICustomerVisitsImportRepository customerVisitsImportRepository;

        public CustomerVisitsImportService(
            ICasinoGameRepository casinoGameRepository,
            ICasinoPlayerTypeRepository casinoPlayerTypeRepository,
            ICustomerTotalBetRangeRepository customerTotalBetRangeRepository,
            ICustomerVisitsImportRepository customerVisitsImportRepository)
        {
            this.casinoGameRepository = casinoGameRepository;
            this.casinoPlayerTypeRepository = casinoPlayerTypeRepository;
            this.customerTotalBetRangeRepository = customerTotalBetRangeRepository;
            this.customerVisitsImportRepository = customerVisitsImportRepository;
        }

        public Task<IReadOnlyList<CustomerVisitsImport>> GetAllWithCollectionsAsNoTrackingAsync()
        {
            var spec = new AllCustomerVisitsImportsWithCollectionsSpecification();

            return this.customerVisitsImportRepository.ListAsNoTrackingAsync(spec);
        }

        public async Task<int> CreateAsync(
            string name,
            IEnumerable<int> customerVisitsCollectionIds,
            IEnumerable<CustomerVisitsDto> customerVisitsDtos)
        {
            IDictionary<string, CasinoGame> casinoGames = await this.casinoGameRepository.AllAsDictionaryAsync();
            IDictionary<string, CasinoPlayerType> casinoPlayerTypes =
                await this.casinoPlayerTypeRepository.AllAsDictionaryAsync();
            IDictionary<string, CustomerTotalBetRange> customerTotalBetRanges =
                await this.customerTotalBetRangeRepository.AllAsDictionaryAsync();

            var comparer = StringComparer.OrdinalIgnoreCase;

            casinoGames = new Dictionary<string, CasinoGame>(casinoGames, comparer);
            casinoPlayerTypes = new Dictionary<string, CasinoPlayerType>(casinoPlayerTypes, comparer);
            customerTotalBetRanges = new Dictionary<string, CustomerTotalBetRange>(customerTotalBetRanges, comparer);

            CustomerVisitsImport customerVisitsImport = new CustomerVisitsImport(name);

            foreach (int customerVisitsCollectionId in customerVisitsCollectionIds)
            {
                var customerVisitsCollectionImport = new CustomerVisitsCollectionImport(
                    customerVisitsCollectionId,
                    customerVisitsImport);

                customerVisitsImport.AddCustomerVisitsCollectionImport(customerVisitsCollectionImport);
            }

            foreach (CustomerVisitsDto customerVisitsDto in customerVisitsDtos)
            {
                CasinoGame casinoGame = null;
                if (customerVisitsDto.PreferGame != null)
                {
                    if (casinoGames.ContainsKey(customerVisitsDto.PreferGame))
                    {
                        casinoGame = casinoGames[customerVisitsDto.PreferGame];
                    }
                    else
                    {
                        casinoGame = new CasinoGame(customerVisitsDto.PreferGame, 999);
                        casinoGames.Add(casinoGame.Name, casinoGame);
                    }
                }

                CasinoPlayerType casinoPlayerType = null;
                if (customerVisitsDto.PlayerType != null)
                {
                    if (casinoPlayerTypes.ContainsKey(customerVisitsDto.PlayerType))
                    {
                        casinoPlayerType = casinoPlayerTypes[customerVisitsDto.PlayerType];
                    }
                    else
                    {
                        casinoPlayerType = new CasinoPlayerType(customerVisitsDto.PlayerType, 999);
                        casinoPlayerTypes.Add(casinoPlayerType.Name, casinoPlayerType);
                    }
                }

                CustomerTotalBetRange customerTotalBetRange = null;
                if (customerVisitsDto.TotalBetRange != null)
                {
                    if (customerTotalBetRanges.ContainsKey(customerVisitsDto.TotalBetRange))
                    {
                        customerTotalBetRange = customerTotalBetRanges[customerVisitsDto.TotalBetRange];
                    }
                    else
                    {
                        customerTotalBetRange = new CustomerTotalBetRange(customerVisitsDto.TotalBetRange, 999);
                        customerTotalBetRanges.Add(customerTotalBetRange.Name, customerTotalBetRange);
                    }
                }

                var customerVisits = CustomerVisits.FromDto(
                    customerVisitsDto,
                    customerVisitsImport,
                    casinoGame,
                    casinoPlayerType,
                    customerTotalBetRange);

                customerVisitsImport.AddCustomerVisits(customerVisits);
            }

            this.customerVisitsImportRepository.Add(customerVisitsImport);

            return await this.customerVisitsImportRepository.SaveChangesAsync();
        }
    }
}
