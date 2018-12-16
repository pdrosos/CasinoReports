namespace CasinoReports.Infrastructure.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialApplicationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Casinos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casinos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CasinoManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CasinoId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasinoManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasinoManager_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasinoManager_Casinos_CasinoId",
                        column: x => x.CasinoId,
                        principalTable: "Casinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionCasino",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CustomerVisitsCollectionId = table.Column<int>(nullable: false),
                    CasinoId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsCollectionCasino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionCasino_Casinos_CasinoId",
                        column: x => x.CasinoId,
                        principalTable: "Casinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionCasino_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CustomerVisitsCollectionId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsCollectionUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionUser_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsImport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CustomerVisitsCollectionId = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsImport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsImport_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CustomerVisitsImportId = table.Column<int>(nullable: true),
                    NameFirstLast = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PreferGame = table.Column<string>(nullable: true),
                    Visits = table.Column<int>(nullable: false),
                    AvgBet = table.Column<decimal>(nullable: true),
                    PlayerType = table.Column<string>(nullable: true),
                    TotalBet = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<int>(nullable: false),
                    BonusFromPoints = table.Column<int>(nullable: false),
                    MatchPay = table.Column<int>(nullable: false),
                    TombolaGame = table.Column<int>(nullable: false),
                    TotalBonuses = table.Column<decimal>(nullable: false),
                    CleanBalance = table.Column<decimal>(nullable: false),
                    BonusPercentOfBet = table.Column<decimal>(nullable: true),
                    BonusPercentOfLose = table.Column<decimal>(nullable: false),
                    PlayPercent = table.Column<bool>(nullable: true),
                    NewCustomers = table.Column<bool>(nullable: true),
                    HoldOnSept = table.Column<bool>(nullable: true),
                    HoldOnOkt = table.Column<bool>(nullable: true),
                    Holded = table.Column<bool>(nullable: false),
                    TotalBetRange = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisits_CustomerVisitsImport_CustomerVisitsImportId",
                        column: x => x.CustomerVisitsImportId,
                        principalTable: "CustomerVisitsImport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManager_ApplicationUserId",
                table: "CasinoManager",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManager_CasinoId",
                table: "CasinoManager",
                column: "CasinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManager_IsDeleted",
                table: "CasinoManager",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Casinos_IsDeleted",
                table: "Casinos",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_CustomerVisitsImportId",
                table: "CustomerVisits",
                column: "CustomerVisitsImportId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_IsDeleted",
                table: "CustomerVisits",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionCasino_CasinoId",
                table: "CustomerVisitsCollectionCasino",
                column: "CasinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionCasino_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionCasino",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionCasino_IsDeleted",
                table: "CustomerVisitsCollectionCasino",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollections_IsDeleted",
                table: "CustomerVisitsCollections",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUser_ApplicationUserId",
                table: "CustomerVisitsCollectionUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUser_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionUser",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUser_IsDeleted",
                table: "CustomerVisitsCollectionUser",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImport_CustomerVisitsCollectionId",
                table: "CustomerVisitsImport",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImport_IsDeleted",
                table: "CustomerVisitsImport",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasinoManager");

            migrationBuilder.DropTable(
                name: "CustomerVisits");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionCasino");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionUser");

            migrationBuilder.DropTable(
                name: "CustomerVisitsImport");

            migrationBuilder.DropTable(
                name: "Casinos");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollections");
        }
    }
}
