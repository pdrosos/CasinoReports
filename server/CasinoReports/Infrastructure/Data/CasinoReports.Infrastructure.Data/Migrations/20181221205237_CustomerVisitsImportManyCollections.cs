namespace CasinoReports.Infrastructure.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CustomerVisitsImportManyCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVisitsImport_CustomerVisitsCollections_CustomerVisitsCollectionId",
                table: "CustomerVisitsImport");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisitsImport_CustomerVisitsCollectionId",
                table: "CustomerVisitsImport");

            migrationBuilder.DropColumn(
                name: "CustomerVisitsCollectionId",
                table: "CustomerVisitsImport");

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionImport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CustomerVisitsCollectionId = table.Column<int>(nullable: false),
                    CustomerVisitsImportId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVisitsCollectionImport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionImport_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionImport_CustomerVisitsImport_CustomerVisitsImportId",
                        column: x => x.CustomerVisitsImportId,
                        principalTable: "CustomerVisitsImport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImport_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionImport",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImport_CustomerVisitsImportId",
                table: "CustomerVisitsCollectionImport",
                column: "CustomerVisitsImportId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImport_IsDeleted",
                table: "CustomerVisitsCollectionImport",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionImport");

            migrationBuilder.AddColumn<int>(
                name: "CustomerVisitsCollectionId",
                table: "CustomerVisitsImport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImport_CustomerVisitsCollectionId",
                table: "CustomerVisitsImport",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVisitsImport_CustomerVisitsCollections_CustomerVisitsCollectionId",
                table: "CustomerVisitsImport",
                column: "CustomerVisitsCollectionId",
                principalTable: "CustomerVisitsCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
