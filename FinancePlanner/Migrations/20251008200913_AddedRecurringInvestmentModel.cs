using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancePlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecurringInvestmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reoccuring",
                table: "Investment");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Investment",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Investment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "Investment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyInDays",
                table: "Investment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Investment",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "FrequencyInDays",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Investment");

            migrationBuilder.AddColumn<bool>(
                name: "Reoccuring",
                table: "Investment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
