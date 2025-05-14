using System.Security.Policy;
using System.ComponentModel.DataAnnotations;

namespace BloomBox.Models
{
    public class AdresaDostave
    {
        [Key]
        public int adresaDostaveId { get; set; }
        public String grad { get; set; }
        public String adresa { get; set; }
        public int postanskiBroj { get; set; }


    }
}
