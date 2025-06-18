using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloomBox.Models
{
    public class Personalizacija
    {
        [Key]
        public int personalizacijaId { get; set; }
        public Double maximalnaCijena { get; set; }
        public String parametar { get; set; }
        [ForeignKey("Proizvod")]
        public int proizvodId { get; set; }
        public Personalizacija() { }
    }
}
