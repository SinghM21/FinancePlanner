using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancePlanner.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPropertiesFromRecurringInvestments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequencyInDays",
                table: "RecurringInvestment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "RecurringInvestment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "RecurringInvestment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyInDays",
                table: "RecurringInvestment",
                type: "int",
                nullable: true);
        }
    }
}
