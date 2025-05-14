using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloomBox.Models
{
    public class ProizvodParametri
    {
        [Key]
        public int proParametriId { get; set; }
        [ForeignKey("Parametri")]
        public int parametarId { get; set; }
        [ForeignKey("Proizvod")]
        public int proizvodId { get; set; }
        public ProizvodParametri() { }

    }
}
