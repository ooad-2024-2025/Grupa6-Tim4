using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class Lokacija
    {
        [Key]
        public int lokacijaId { get; set; }
        public String lokacijaKorisnikaURL { get; set; }
        public String lokacijaURL { get; set; }
        public Lokacija() { }
    }
}
