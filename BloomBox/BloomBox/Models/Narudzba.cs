using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models

{
    public class Narudzba
    {
    [Key]
        public int narudzbaId { get; set; }
    public DateTime datum { get; set; }
    public static int zadnjiBrojNarudzbe { get; set; }
    public String adresaDostave { get; set; }
    public Double ukupnaCijena { get; set; }

    public Narudzba() { }


}
}

