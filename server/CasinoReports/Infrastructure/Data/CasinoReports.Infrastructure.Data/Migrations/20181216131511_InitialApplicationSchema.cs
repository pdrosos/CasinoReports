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
                name: "CasinoManagers",
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
                    table.PrimaryKey("PK_CasinoManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasinoManagers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasinoManagers_Casinos_CasinoId",
                        column: x => x.CasinoId,
                        principalTable: "Casinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionCasinos",
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
                    table.PrimaryKey("PK_CustomerVisitsCollectionCasinos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionCasinos_Casinos_CasinoId",
                        column: x => x.CasinoId,
                        principalTable: "Casinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionCasinos_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionUsers",
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
                    table.PrimaryKey("PK_CustomerVisitsCollectionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionUsers_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVisitsImports",
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
                    table.PrimaryKey("PK_CustomerVisitsImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsImports_CustomerVisitsCollections_CustomerVisitsCollectionId",
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
                        name: "FK_CustomerVisits_CustomerVisitsImports_CustomerVisitsImportId",
                        column: x => x.CustomerVisitsImportId,
                        principalTable: "CustomerVisitsImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManagers_ApplicationUserId",
                table: "CasinoManagers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManagers_CasinoId",
                table: "CasinoManagers",
                column: "CasinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoManagers_IsDeleted",
                table: "CasinoManagers",
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
                name: "IX_CustomerVisitsCollectionCasinos_CasinoId",
                table: "CustomerVisitsCollectionCasinos",
                column: "CasinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionCasinos_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionCasinos",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionCasinos_IsDeleted",
                table: "CustomerVisitsCollectionCasinos",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollections_IsDeleted",
                table: "CustomerVisitsCollections",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUsers_ApplicationUserId",
                table: "CustomerVisitsCollectionUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUsers_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionUsers",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionUsers_IsDeleted",
                table: "CustomerVisitsCollectionUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImports_CustomerVisitsCollectionId",
                table: "CustomerVisitsImports",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImports_IsDeleted",
                table: "CustomerVisitsImports",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasinoManagers");

            migrationBuilder.DropTable(
                name: "CustomerVisits");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionCasinos");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionUsers");

            migrationBuilder.DropTable(
                name: "CustomerVisitsImports");

            migrationBuilder.DropTable(
                name: "Casinos");

            migrationBuilder.DropTable(
                name: "CustomerVisitsCollections");
        }
    }
}
