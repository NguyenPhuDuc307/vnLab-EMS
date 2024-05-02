using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vnLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDatabase8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "SalarySheets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GrossSalary",
                table: "SalarySheets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
