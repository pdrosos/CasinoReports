namespace CasinoReports.Infrastructure.Data.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CasinoReports.Core.Models.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;

    public class DatabaseInitializer
    {
        public static bool AllMigrationsApplied(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var applied = dbContext.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = dbContext.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void SeedDatabase(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            if (applicationDbContext == null)
            {
                throw new ArgumentNullException(nameof(applicationDbContext));
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            applicationDbContext.Database.Migrate();
            bool allMigrationsApplied = AllMigrationsApplied(applicationDbContext);
            if (allMigrationsApplied)
            {
                SeedRoles(applicationDbContext, roleManager);
                SeedUsers(applicationDbContext, userManager);
                SeedData(applicationDbContext);
            }
        }

        private static void SeedRoles(
            ApplicationDbContext applicationDbContext,
            RoleManager<ApplicationRole> roleManager)
        {
            if (!applicationDbContext.Roles.Any())
            {
                var roles = new List<ApplicationRole>()
                {
                    new ApplicationRole(ApplicationRole.Administrator),
                    new ApplicationRole(ApplicationRole.ChiefManager),
                    new ApplicationRole(ApplicationRole.CasinoManager),
                };

                foreach (var role in roles)
                {
                    IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static void SeedUsers(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager)
        {
            if (!applicationDbContext.Users.Any())
            {
                var password = "Abc123=";

                // create administrator
                ApplicationUser administrator = CreateUser(
                    userManager,
                    "administrator@example.com",
                    "administrator@example.com",
                    password);
                AddUserToRole(userManager, administrator, ApplicationRole.Administrator);

                // create chief manager
                ApplicationUser chiefManager = CreateUser(
                    userManager,
                    "chiefmanager@example.com",
                    "chiefmanager@example.com",
                    password);
                AddUserToRole(userManager, chiefManager, ApplicationRole.ChiefManager);

                // create casino manager
                ApplicationUser casinoManager = CreateUser(
                    userManager,
                    "casinomanager@example.com",
                    "casinomanager@example.com",
                    password);
                AddUserToRole(userManager, casinoManager, ApplicationRole.CasinoManager);
            }
        }

        private static void SeedData(ApplicationDbContext applicationDbContext)
        {
            if (!applicationDbContext.Casinos.Any() && !applicationDbContext.CustomerVisitsCollections.Any())
            {
                // Create casinos
                var casino1 = new Casino("Casino 1");
                var casino2 = new Casino("Casino 2");
                var casino3 = new Casino("Casino 3");

                var casinos = new List<Casino>();
                casinos.Add(casino1);
                casinos.Add(casino2);
                casinos.Add(casino3);

                applicationDbContext.Casinos.AddRange(casinos);
                applicationDbContext.SaveChanges();

                // Create customer visits collections and add casinos to them
                var collectionCasino12018 = new CustomerVisitsCollection("Casino 1 2018");
                collectionCasino12018.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino12018, casino1));

                var collectionCasino22018 = new CustomerVisitsCollection("Casino 2 2018");
                collectionCasino22018.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino22018, casino2));

                var collectionCasino32018 = new CustomerVisitsCollection("Casino 3 2018");
                collectionCasino32018.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino32018, casino3));

                var collectionAll2018 = new CustomerVisitsCollection("All 2018");

                var collectionCasino12019 = new CustomerVisitsCollection("Casino 1 2019");
                collectionCasino12019.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino12019, casino1));

                var collectionCasino22019 = new CustomerVisitsCollection("Casino 2 2019");
                collectionCasino22019.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino22019, casino2));

                var collectionCasino32019 = new CustomerVisitsCollection("Casino 3 2019");
                collectionCasino32019.AddCustomerVisitsCollectionCasino(
                    new CustomerVisitsCollectionCasino(collectionCasino32019, casino3));

                var collectionAll2019 = new CustomerVisitsCollection("All 2019");

                var collections = new List<CustomerVisitsCollection>();
                collections.Add(collectionCasino12018);
                collections.Add(collectionCasino22018);
                collections.Add(collectionCasino32018);
                collections.Add(collectionAll2018);
                collections.Add(collectionCasino12019);
                collections.Add(collectionCasino22019);
                collections.Add(collectionCasino32019);
                collections.Add(collectionAll2019);

                applicationDbContext.CustomerVisitsCollections.AddRange(collections);
                applicationDbContext.SaveChanges();
            }

            // Create casino games
            if (!applicationDbContext.CasinoGames.Any())
            {
                var bj = new CasinoGame("BJ", 1);
                var roulette = new CasinoGame("Roulette", 2);
                var slots = new CasinoGame("Slots", 3);

                var casinoGames = new List<CasinoGame>();
                casinoGames.Add(bj);
                casinoGames.Add(roulette);
                casinoGames.Add(slots);

                applicationDbContext.CasinoGames.AddRange(casinoGames);
                applicationDbContext.SaveChanges();
            }

            // Create casino player types
            if (!applicationDbContext.CasinoPlayerTypes.Any())
            {
                var newbie = new CasinoPlayerType("Новак", 1);
                var weak = new CasinoPlayerType("Слаб", 2);
                var average = new CasinoPlayerType("Среден", 3);
                var good = new CasinoPlayerType("Хубав", 4);
                var strong = new CasinoPlayerType("Силен", 5);
                var special = new CasinoPlayerType("Специален", 6);

                var casinoPlayerTypes = new List<CasinoPlayerType>();
                casinoPlayerTypes.Add(newbie);
                casinoPlayerTypes.Add(weak);
                casinoPlayerTypes.Add(average);
                casinoPlayerTypes.Add(good);
                casinoPlayerTypes.Add(strong);
                casinoPlayerTypes.Add(special);

                applicationDbContext.CasinoPlayerTypes.AddRange(casinoPlayerTypes);
                applicationDbContext.SaveChanges();
            }

            // Create customer total bet ranges
            if (!applicationDbContext.CustomerTotalBetRanges.Any())
            {
                var range1 = new CustomerTotalBetRange("0-100", 1);
                var range2 = new CustomerTotalBetRange("100-500", 2);
                var range3 = new CustomerTotalBetRange("500-1000", 3);
                var range4 = new CustomerTotalBetRange("1000-5000", 4);
                var range5 = new CustomerTotalBetRange("5000-20000", 5);
                var range6 = new CustomerTotalBetRange("20000-100000", 6);
                var range7 = new CustomerTotalBetRange("Over 100000", 7);

                var customerTotalBetRanges = new List<CustomerTotalBetRange>();
                customerTotalBetRanges.Add(range1);
                customerTotalBetRanges.Add(range2);
                customerTotalBetRanges.Add(range3);
                customerTotalBetRanges.Add(range4);
                customerTotalBetRanges.Add(range5);
                customerTotalBetRanges.Add(range6);
                customerTotalBetRanges.Add(range7);

                applicationDbContext.CustomerTotalBetRanges.AddRange(customerTotalBetRanges);
                applicationDbContext.SaveChanges();
            }
        }

        private static ApplicationUser CreateUser(
            UserManager<ApplicationUser> userManager,
            string userName,
            string email,
            string password)
        {
            var user = new ApplicationUser(userName);
            user.Email = email;
            user.EmailConfirmed = true;

            IdentityResult result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        private static void AddUserToRole(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string role)
        {
            IdentityResult result = userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
