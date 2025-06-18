using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomBox.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddKupacForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "ProizvodKorpa",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "Narudzba",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodKorpa_KupacId",
                table: "ProizvodKorpa",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KupacId",
                table: "Narudzba",
                column: "KupacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_AspNetUsers_KupacId",
                table: "Narudzba",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_AspNetUsers_KupacId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodKorpa_KupacId",
                table: "ProizvodKorpa");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_KupacId",
                table: "Narudzba");

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "ProizvodKorpa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KupacId",
                table: "Narudzba",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
