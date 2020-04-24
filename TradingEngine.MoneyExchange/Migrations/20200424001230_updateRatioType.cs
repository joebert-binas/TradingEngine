using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingEngine.MoneyExchange.Migrations
{
    public partial class updateRatioType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Ratio",
                table: "Currencies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ratio",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
