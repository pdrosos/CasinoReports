﻿// <auto-generated />
using System;
using CasinoReports.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CasinoReports.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181230165005_CustomerVisitsRelatedTables")]
    partial class CustomerVisitsRelatedTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.Casino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Casinos");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CasinoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CasinoGames");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CasinoManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<int>("CasinoId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CasinoId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CasinoManagers");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CasinoPlayerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CasinoPlayerTypes");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerTotalBetRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerTotalBetRanges");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisits", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AvgBet")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("Bonus");

                    b.Property<int>("BonusFromPoints");

                    b.Property<decimal?>("BonusPercentOfBet")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<decimal>("BonusPercentOfLose")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<decimal>("CleanBalance")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int?>("CustomerVisitsImportId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool?>("HoldOnOkt");

                    b.Property<bool?>("HoldOnSept");

                    b.Property<bool>("Holded");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MatchPay");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NameFirstLast");

                    b.Property<bool?>("NewCustomers");

                    b.Property<bool?>("PlayPercent");

                    b.Property<int?>("PlayerTypeId");

                    b.Property<int?>("PreferGameId");

                    b.Property<int>("TombolaGame");

                    b.Property<decimal>("TotalBet")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<int?>("TotalBetRangeId");

                    b.Property<decimal>("TotalBonuses")
                        .HasColumnType("decimal(22, 10)");

                    b.Property<int>("Visits");

                    b.HasKey("Id");

                    b.HasIndex("CustomerVisitsImportId");

                    b.HasIndex("Date");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PlayerTypeId");

                    b.HasIndex("PreferGameId");

                    b.HasIndex("TotalBetRangeId");

                    b.ToTable("CustomerVisits");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerVisitsCollections");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionCasino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CasinoId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CustomerVisitsCollectionId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("CasinoId");

                    b.HasIndex("CustomerVisitsCollectionId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerVisitsCollectionCasinos");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionImport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CustomerVisitsCollectionId");

                    b.Property<int>("CustomerVisitsImportId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("CustomerVisitsCollectionId");

                    b.HasIndex("CustomerVisitsImportId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerVisitsCollectionImports");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CustomerVisitsCollectionId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CustomerVisitsCollectionId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerVisitsCollectionUsers");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsImport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("CustomerVisitsImports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CasinoManager", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("CasinoManagers")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CasinoReports.Core.Models.Entities.Casino", "Casino")
                        .WithMany("CasinoManagers")
                        .HasForeignKey("CasinoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisits", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerVisitsImport", "CustomerVisitsImport")
                        .WithMany("CustomerVisits")
                        .HasForeignKey("CustomerVisitsImportId");

                    b.HasOne("CasinoReports.Core.Models.Entities.CasinoPlayerType", "PlayerType")
                        .WithMany()
                        .HasForeignKey("PlayerTypeId");

                    b.HasOne("CasinoReports.Core.Models.Entities.CasinoGame", "PreferGame")
                        .WithMany()
                        .HasForeignKey("PreferGameId");

                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerTotalBetRange", "TotalBetRange")
                        .WithMany()
                        .HasForeignKey("TotalBetRangeId");
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionCasino", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.Casino", "Casino")
                        .WithMany("CustomerVisitsCollectionCasinos")
                        .HasForeignKey("CasinoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerVisitsCollection", "CustomerVisitsCollection")
                        .WithMany("CustomerVisitsCollectionCasinos")
                        .HasForeignKey("CustomerVisitsCollectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionImport", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerVisitsCollection", "CustomerVisitsCollection")
                        .WithMany("CustomerVisitsCollectionImports")
                        .HasForeignKey("CustomerVisitsCollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerVisitsImport", "CustomerVisitsImport")
                        .WithMany("CustomerVisitsCollectionImports")
                        .HasForeignKey("CustomerVisitsImportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CasinoReports.Core.Models.Entities.CustomerVisitsCollectionUser", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("CustomerVisitsCollectionUsers")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CasinoReports.Core.Models.Entities.CustomerVisitsCollection", "CustomerVisitsCollection")
                        .WithMany("CustomerVisitsCollectionUsers")
                        .HasForeignKey("CustomerVisitsCollectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser")
                        .WithMany("IdentityUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser")
                        .WithMany("IdentityUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser")
                        .WithMany("IdentityUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CasinoReports.Core.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
