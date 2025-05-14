using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloomBox.Models
{
    public class Placanje
    {
        [Key]
        public int placanjeId {  get; set; }
        [ForeignKey("Narudzba")]
        public int narudzbaId { get; set; }
        public DateTime datum {  get; set; }
        public Status status { get; set; }
        public String transakcijskiId { get; set; }
    }
}
