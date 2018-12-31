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
                name: "FK_CustomerVisitsImports_CustomerVisitsCollections_CustomerVisitsCollectionId",
                table: "CustomerVisitsImports");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVisitsImports_CustomerVisitsCollectionId",
                table: "CustomerVisitsImports");

            migrationBuilder.DropColumn(
                name: "CustomerVisitsCollectionId",
                table: "CustomerVisitsImports");

            migrationBuilder.CreateTable(
                name: "CustomerVisitsCollectionImports",
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
                    table.PrimaryKey("PK_CustomerVisitsCollectionImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionImports_CustomerVisitsCollections_CustomerVisitsCollectionId",
                        column: x => x.CustomerVisitsCollectionId,
                        principalTable: "CustomerVisitsCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVisitsCollectionImports_CustomerVisitsImports_CustomerVisitsImportId",
                        column: x => x.CustomerVisitsImportId,
                        principalTable: "CustomerVisitsImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImports_CustomerVisitsCollectionId",
                table: "CustomerVisitsCollectionImports",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImports_CustomerVisitsImportId",
                table: "CustomerVisitsCollectionImports",
                column: "CustomerVisitsImportId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsCollectionImports_IsDeleted",
                table: "CustomerVisitsCollectionImports",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerVisitsCollectionImports");

            migrationBuilder.AddColumn<int>(
                name: "CustomerVisitsCollectionId",
                table: "CustomerVisitsImports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVisitsImports_CustomerVisitsCollectionId",
                table: "CustomerVisitsImports",
                column: "CustomerVisitsCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVisitsImports_CustomerVisitsCollections_CustomerVisitsCollectionId",
                table: "CustomerVisitsImports",
                column: "CustomerVisitsCollectionId",
                principalTable: "CustomerVisitsCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
