namespace CasinoReports.Infrastructure.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CustomerVisitsRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerType",
                table: "CustomerVisits");

            migrationBuilder.DropColumn(
                name: "PreferGame",
                table: "CustomerVisits");

            migrationBuilder.DropColumn(
                name: "TotalBetRange",
                table: "CustomerVisits");

            migrationBuilder.AddColumn<int>(
                name: "PlayerTypeId",
                table: "CustomerVisits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferGameId",
                table: "CustomerVisits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalBetRangeId",
                table: "CustomerVisits",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CasinoGames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasinoGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CasinoPlayerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasinoPlayerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTotalBetRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTotalBetRanges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_Date",
                table: "CustomerVisits",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_PlayerTypeId",
                table: "CustomerVisits",
                column: "PlayerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_PreferGameId",
                table: "CustomerVisits",
                column: "PreferGameId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisits_TotalBetRangeId",
                table: "CustomerVisits",
                column: "TotalBetRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoGames_IsDeleted",
                table: "CasinoGames",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CasinoPlayerTypes_IsDeleted",
                table: "CasinoPlayerTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTotalBetRanges_IsDeleted",
                table: "CustomerTotalBetRanges",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVisits_CasinoPlayerTypes_PlayerTypeId",
                table: "CustomerVisits",
                column: "PlayerTypeId",
                principalTable: "CasinoPlayerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVisits_CasinoGames_PreferGameId",
                table: "CustomerVisits",
                column: "PreferGameId",
                principalTable: "CasinoGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVisits_CustomerTotalBetRanges_TotalBetRangeId",
                table: "CustomerVisits",
                column: "TotalBetRangeId",
                principalTable: "CustomerTotalBetRanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVisits_CasinoPlayerTypes_PlayerTypeId",
                table: "CustomerVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVisits_CasinoGames_PreferGameId",
                table: "CustomerVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVisits_CustomerTotalBetRanges_TotalBetRangeId",
                table: "CustomerVisits");

            migrationBuilder.DropTable(
                name: "CasinoGames");

            migrationBuilder.DropTable(
                name: "CasinoPlayerTypes");

            migrationBuilder.DropTable(
                name: "CustomerTotalBetRanges");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisits_Date",
                table: "CustomerVisits");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisits_PlayerTypeId",
                table: "CustomerVisits");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisits_PreferGameId",
                table: "CustomerVisits");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisits_TotalBetRangeId",
                table: "CustomerVisits");

            migrationBuilder.DropColumn(
                name: "PlayerTypeId",
                table: "CustomerVisits");

            migrationBuilder.DropColumn(
                name: "PreferGameId",
                table: "CustomerVisits");

            migrationBuilder.DropColumn(
                name: "TotalBetRangeId",
                table: "CustomerVisits");

            migrationBuilder.AddColumn<string>(
                name: "PlayerType",
                table: "CustomerVisits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferGame",
                table: "CustomerVisits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalBetRange",
                table: "CustomerVisits",
                nullable: true);
        }
    }
}
