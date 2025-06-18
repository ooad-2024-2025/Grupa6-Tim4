using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomBox.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdresaDostaveMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdresaDostave",
                columns: table => new
                {
                    adresaDostaveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postanskiBroj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdresaDostave", x => x.adresaDostaveId);
                });

            migrationBuilder.CreateTable(
                name: "Lokacija",
                columns: table => new
                {
                    lokacijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lokacijaKorisnikaURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lokacijaURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacija", x => x.lokacijaId);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    narudzbaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    adresaDostave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ukupnaCijena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.narudzbaId);
                });

            migrationBuilder.CreateTable(
                name: "Parametri",
                columns: table => new
                {
                    parametarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametri", x => x.parametarId);
                });

            migrationBuilder.CreateTable(
                name: "Personalizacija",
                columns: table => new
                {
                    personalizacijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maximalnaCijena = table.Column<double>(type: "float", nullable: false),
                    parametar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    proizvodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalizacija", x => x.personalizacijaId);
                });

            migrationBuilder.CreateTable(
                name: "Placanje",
                columns: table => new
                {
                    placanjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    narudzbaId = table.Column<int>(type: "int", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    transakcijskiId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placanje", x => x.placanjeId);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    proizvodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    uvodznikId = table.Column<int>(type: "int", nullable: false),
                    kategorija = table.Column<int>(type: "int", nullable: false),
                    slikaURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kolicinaNaStanju = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.proizvodId);
                });

            migrationBuilder.CreateTable(
                name: "ProizvodKorpa",
                columns: table => new
                {
                    proizvodKorpaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proizvodId = table.Column<int>(type: "int", nullable: false),
                    narudzbaId = table.Column<int>(type: "int", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProizvodKorpa", x => x.proizvodKorpaId);
                });

            migrationBuilder.CreateTable(
                name: "ProizvodParametri",
                columns: table => new
                {
                    proParametriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parametarId = table.Column<int>(type: "int", nullable: false),
                    proizvodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProizvodParametri", x => x.proParametriId);
                });

            migrationBuilder.CreateTable(
                name: "ValidacijaProizvoda",
                columns: table => new
                {
                    validacijaProizvodaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    stanjeProizvoda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidacijaProizvoda", x => x.validacijaProizvodaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdresaDostave");

            migrationBuilder.DropTable(
                name: "Lokacija");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Parametri");

            migrationBuilder.DropTable(
                name: "Personalizacija");

            migrationBuilder.DropTable(
                name: "Placanje");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "ProizvodKorpa");

            migrationBuilder.DropTable(
                name: "ProizvodParametri");

            migrationBuilder.DropTable(
                name: "ValidacijaProizvoda");
        }
    }
}
