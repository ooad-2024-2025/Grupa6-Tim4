using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class Proizvod

    {
        [Key]
        public int proizvodId { get; set; }
        public String ime { get; set; }
        public Double cijena { get; set; }
        public int uvodznikId { get; set; }
        public Kategorija kategorija { get; set; }
        public String slikaURL { get; set; }
        public String opis { get; set; }
        public int kolicinaNaStanju { get; set; }


    }
}
