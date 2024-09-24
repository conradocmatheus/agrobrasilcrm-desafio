using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end.Migrations
{
    /// <inheritdoc />
    public partial class migrationteste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovementProducts_Transactions_MovementId",
                table: "MovementProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Movements");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Movements",
                newName: "IX_Movements_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movements",
                table: "Movements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovementProducts_Movements_MovementId",
                table: "MovementProducts",
                column: "MovementId",
                principalTable: "Movements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Users_UserId",
                table: "Movements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovementProducts_Movements_MovementId",
                table: "MovementProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Users_UserId",
                table: "Movements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movements",
                table: "Movements");

            migrationBuilder.RenameTable(
                name: "Movements",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Movements_UserId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovementProducts_Transactions_MovementId",
                table: "MovementProducts",
                column: "MovementId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
