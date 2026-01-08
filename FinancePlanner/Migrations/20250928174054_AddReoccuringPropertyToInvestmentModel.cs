using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancePlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddReoccuringPropertyToInvestmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reoccuring",
                table: "Investment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reoccuring",
                table: "Investment");
        }
    }
}
