using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancePlanner.Migrations
{
    /// <inheritdoc />
    public partial class MergeInvestmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringInvestment");

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

            migrationBuilder.AddColumn<bool>(
                name: "Recurring",
                table: "Investment",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "EndDate",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "Recurring",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Investment");

            migrationBuilder.CreateTable(
                name: "RecurringInvestment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringInvestment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecurringInvestment_Investment_ID",
                        column: x => x.ID,
                        principalTable: "Investment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
