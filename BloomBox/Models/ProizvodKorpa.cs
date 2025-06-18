using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class ProizvodKorpa
    {
        [Key]
        public int proizvodKorpaId {  get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodId { get; set; }
        public Proizvod Proizvod { get; set; } //navigation property for product

        [ForeignKey("Narudzba")]
        public int? narudzbaId { get; set; }
        public Narudzba? Narudzba { get; set; } //navigation property for order

        public string? SessionId { get; set; }

        [ForeignKey("Korisnik")]
        public String? KupacId { get; set; }
        public Korisnik? Kupac { get; set; } //navigation property for user
        public int kolicina {  get; set; }
        public ProizvodKorpa() { }
    }
}
