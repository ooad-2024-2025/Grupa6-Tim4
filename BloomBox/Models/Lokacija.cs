using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class Lokacija
    {
        [Key]
        public int lokacijaId { get; set; }
        /*public String lokacijaKorisnikaURL { get; set; }
        public String lokacijaURL { get; set; } */
        public String Adresa { get; set; }
        public String Grad { get; set; }

        [Display(Name = "Geografska širina")]
        public double GeografskaSirina { get; set; }

        [Display(Name = "Geografska dužina")]
        public double GeografskaDuzina { get; set; }
        public Lokacija() { }
    }
}

