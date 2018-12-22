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
                // create casinos
                var casino1 = new Casino("Casino 1");
                var casino2 = new Casino("Casino 2");
                var casino3 = new Casino("Casino 3");

                var casinos = new List<Casino>();
                casinos.Add(casino1);
                casinos.Add(casino2);
                casinos.Add(casino3);

                applicationDbContext.Casinos.AddRange(casinos);
                applicationDbContext.SaveChanges();

                // create customer visits collections and add casinos to them
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
