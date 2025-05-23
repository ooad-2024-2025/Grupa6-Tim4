using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace BloomBox.Models
{
    public class Korisnik : IdentityUser
    {

        [Required]
        [Display(Name = "Ime:")]
        public String Ime { get; set; }
        [Required]
        [Display(Name = "Ime:")]
        public String Prezime { get; set; }



    }
}
