namespace CasinoReports.Infrastructure.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DecimalPropertiesColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBonuses",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBet",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "CleanBalance",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusPercentOfLose",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusPercentOfBet",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "AvgBet",
                table: "CustomerVisits",
                type: "decimal(22, 10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBonuses",
                table: "CustomerVisits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBet",
                table: "CustomerVisits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CleanBalance",
                table: "CustomerVisits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusPercentOfLose",
                table: "CustomerVisits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusPercentOfBet",
                table: "CustomerVisits",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "CustomerVisits",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvgBet",
                table: "CustomerVisits",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 10)",
                oldNullable: true);
        }
    }
}
