using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloomBox.Models

{
    public class Narudzba
    {
    [Key]
        public int narudzbaId { get; set; }
    public DateTime datum { get; set; }
    public static int zadnjiBrojNarudzbe { get; set; }

        [ForeignKey("AdresaDostave")]
        public int adresaDostaveId { get; set; }
        public AdresaDostave adresaDostave { get; set; } //navigation property-chat navodno kaze da je ovo dobro jer je lakse tako dohvatiti atribute adrese dostave
        public Double ukupnaCijena { get; set; }

        
    [ForeignKey("Korisnik")]
    public String KupacId { get; set; }
    public Korisnik Kupac { get; set; } 
        public Narudzba() { }


}
}

