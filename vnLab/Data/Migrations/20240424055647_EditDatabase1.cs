using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vnLab.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Contracts_ContractId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContractId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Contracts",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_UserId",
                table: "Contracts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_UserId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContractId",
                table: "Users",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Contracts_ContractId",
                table: "Users",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");
        }
    }
}
