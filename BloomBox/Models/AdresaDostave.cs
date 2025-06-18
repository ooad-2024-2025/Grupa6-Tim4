using System.Security.Policy;
using System.ComponentModel.DataAnnotations;

namespace BloomBox.Models
{
    public class AdresaDostave
    {
        [Key]
        [Display(Name = "Sifra adrese dostave")]
        public int adresaDostaveId { get; set; }

        [Required(ErrorMessage = "Naziv grada je potreban!")]
        [StringLength(50, ErrorMessage = "Naziv grada ne smije da prevazilazi 50 karaktera!")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "Naziv grada moze da sadrzi samo slova, razmake i apostrofe!")]
        public String grad { get; set; }

        [Required(ErrorMessage = "Adresa je potrebna!")]
        [StringLength(100, ErrorMessage = "Naziv adrese ne smije prevazilati 100 karaktera!")]
        public String adresa { get; set; }

        [Display(Name = "Postanski broj")]
        [Required(ErrorMessage = "Postanski broj je potreban!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Nevalidan format postanskog broja!")]
        public int postanskiBroj { get; set; }

        public AdresaDostave() { }

    }
}
