using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vnLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDayOfMoth",
                table: "TimeSheets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastDayOfMoth",
                table: "TimeSheets",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
