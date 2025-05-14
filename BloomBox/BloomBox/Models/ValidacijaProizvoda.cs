using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class ValidacijaProizvoda
    {
        [Key]
        public int validacijaProizvodaId { get; set; }
        public Boolean status {  get; set; }
        public int stanjeProizvoda { get; set; }
    }
}
