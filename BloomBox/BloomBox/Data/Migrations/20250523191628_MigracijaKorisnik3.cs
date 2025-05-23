using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomBox.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracijaKorisnik3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "AspNetUsers",
                newName: "imePrezime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imePrezime",
                table: "AspNetUsers",
                newName: "Prezime");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
