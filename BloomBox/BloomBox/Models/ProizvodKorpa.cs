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
        [ForeignKey("Narudzba")]
        public int narudzbaId { get; set; }
        public int kolicina {  get; set; }
    }
}
