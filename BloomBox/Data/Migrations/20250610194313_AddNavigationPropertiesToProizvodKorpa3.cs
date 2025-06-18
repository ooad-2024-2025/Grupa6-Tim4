using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomBox.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertiesToProizvodKorpa3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa");

            migrationBuilder.AlterColumn<int>(
                name: "narudzbaId",
                table: "ProizvodKorpa",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodKorpa_narudzbaId",
                table: "ProizvodKorpa",
                column: "narudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodKorpa_proizvodId",
                table: "ProizvodKorpa",
                column: "proizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodKorpa_Narudzba_narudzbaId",
                table: "ProizvodKorpa",
                column: "narudzbaId",
                principalTable: "Narudzba",
                principalColumn: "narudzbaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodKorpa_Proizvod_proizvodId",
                table: "ProizvodKorpa",
                column: "proizvodId",
                principalTable: "Proizvod",
                principalColumn: "proizvodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa");

            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodKorpa_Narudzba_narudzbaId",
                table: "ProizvodKorpa");

            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodKorpa_Proizvod_proizvodId",
                table: "ProizvodKorpa");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodKorpa_narudzbaId",
                table: "ProizvodKorpa");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodKorpa_proizvodId",
                table: "ProizvodKorpa");

            migrationBuilder.AlterColumn<int>(
                name: "narudzbaId",
                table: "ProizvodKorpa",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodKorpa_AspNetUsers_KupacId",
                table: "ProizvodKorpa",
                column: "KupacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
