using BloomBox.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloomBox.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //dodano
        public DbSet<AdresaDostave> AdresaDostave { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Parametri> Parametri { get; set; }
        public DbSet<Personalizacija> Personalizacija { get; set; }
        public DbSet<Placanje> Placanje { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<ProizvodKorpa> ProizvodKorpa { get; set; }
        public DbSet<ProizvodParametri> ProizvodParametri { get; set; }
        public DbSet<ValidacijaProizvoda> ValidacijaProizvoda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdresaDostave>().ToTable("AdresaDostave");
            modelBuilder.Entity<Lokacija>().ToTable("Lokacija");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Parametri>().ToTable("Parametri");
            modelBuilder.Entity<Personalizacija>().ToTable("Personalizacija");
            modelBuilder.Entity<Placanje>().ToTable("Placanje");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<ProizvodKorpa>().ToTable("ProizvodKorpa");
            modelBuilder.Entity<ProizvodParametri>().ToTable("ProizvodParametri");
            modelBuilder.Entity<ValidacijaProizvoda>().ToTable("ValidacijaProizvoda");
            base.OnModelCreating(modelBuilder);

            // Configure existing AdresaDostave relationship
            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.adresaDostave)
                .WithMany()
                .HasForeignKey(n => n.adresaDostaveId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Kupac relationship in Narudzba
            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Kupac)
                .WithMany()
                .HasForeignKey(n => n.KupacId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure nullable Kupac relationship in ProizvodKorpa
           /* modelBuilder.Entity<ProizvodKorpa>()
                .HasOne(p => p.Kupac)
                .WithMany()
                .HasForeignKey(p => p.KupacId)
                .IsRequired(false) // Makes the relationship optional
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProizvodKorpa>()
                .HasOne(pk => pk.Proizvod)
                .WithMany(p=>p.ProizvodKorpaItems)
                .HasForeignKey(p => p.proizvodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProizvodKorpa>()
                .HasOne(p => p.Narudzba)
                .WithMany()
                .HasForeignKey(p => p.narudzbaId)
                .IsRequired(false) // Makes the relationship optional
                .OnDelete(DeleteBehavior.Restrict);
           */

        }
    }
}
