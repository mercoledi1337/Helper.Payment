using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Payments.Core.Migrations
{
    /// <inheritdoc />
    public partial class ispaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "payment",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "payment",
                table: "Invoices");
        }
    }
}
