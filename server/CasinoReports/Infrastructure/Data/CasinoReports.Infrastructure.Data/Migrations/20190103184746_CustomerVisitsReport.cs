namespace CasinoReports.Infrastructure.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CustomerVisitsReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerVisitsReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Settings = table.Column<string>(type: "ntext", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsReports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsReports_IsDeleted",
                table: "CustomerVisitsReports",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerVisitsReports");
        }
    }
}
