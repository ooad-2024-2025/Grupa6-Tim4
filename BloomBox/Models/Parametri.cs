using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models

{
    public class Parametri
    {
        [Key]
        public int parametarId { get; set; }
        public String ime { get; set; }
        public Parametri() { }
    }
}
