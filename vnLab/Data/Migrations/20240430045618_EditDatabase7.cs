using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vnLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDatabase7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Positions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Divisions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Divisions");
        }
    }
}
