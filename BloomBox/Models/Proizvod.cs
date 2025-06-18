using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class Proizvod

    {
        [DisplayName("Sifra proizvoda")]
        [Key]
        public int proizvodId { get; set; }

        [DisplayName("Ime")]
        [Required(ErrorMessage = "Naziv proizvoda je obavezan")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv mora imati između 2 i 100 znakova")]
        [RegularExpression(@"^[a-zA-ZčćžšđČĆŽŠĐ0-9\s\-\.\,]+$",
            ErrorMessage = "Dozvoljeni su samo slova, brojevi, razmaci i osnovni interpunkcijski znaci")]
        public String ime { get; set; }

        [DisplayName("Cijena")]
        [Required(ErrorMessage = "Cijena je obavezna")]
        [Range(0.01, 100000, ErrorMessage = "Cijena mora biti između 0.01 i 100.000")]
        public Double cijena { get; set; }

        [DisplayName("Sifra uvoznika")]
        [Required(ErrorMessage = "Šifra uvoznika je obavezna")]
        [Range(1, int.MaxValue, ErrorMessage = "Nevažeća šifra uvoznika")]
        public int uvodznikId { get; set; }


        [DisplayName("Kategorija")]
        [Required(ErrorMessage = "Kategorija je obavezna")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija kategorija { get; set; }


        [DisplayName("URL slike")]
        public String slikaURL { get; set; }

        [DisplayName("Opis")]
        [StringLength(200000, ErrorMessage = "Opis ne smije biti duži od 200000 znakova")]
        public String opis { get; set; }


        [DisplayName("Kolicina proizvoda")]
        [Required(ErrorMessage = "Količina na stanju je obavezna")]
        [Range(0, 10000, ErrorMessage = "Količina mora biti između 0 i 10.000")]
        public int kolicinaNaStanju { get; set; }
        public Proizvod() { }


    }
}
