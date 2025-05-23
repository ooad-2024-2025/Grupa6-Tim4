using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomBox.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracijaKorisnik2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prezime",
                table: "AspNetUsers",
                newName: "Prezime");

            migrationBuilder.RenameColumn(
                name: "ime",
                table: "AspNetUsers",
                newName: "Ime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "AspNetUsers",
                newName: "prezime");

            migrationBuilder.RenameColumn(
                name: "Ime",
                table: "AspNetUsers",
                newName: "ime");
        }
    }
}
