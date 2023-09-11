using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Payments.Core.Migrations
{
    /// <inheritdoc />
    public partial class dateInIvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "payment",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountNumber",
                schema: "payment",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RealisationEnd",
                schema: "payment",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RealisationStart",
                schema: "payment",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealisationEnd",
                schema: "payment",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RealisationStart",
                schema: "payment",
                table: "Invoices");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "payment",
                table: "Invoices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountNumber",
                schema: "payment",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
