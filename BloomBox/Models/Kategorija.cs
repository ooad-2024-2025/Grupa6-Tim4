using System.ComponentModel.DataAnnotations;

namespace BloomBox.Models
{
    public enum Kategorija
    {
        [Display(Name ="Cvjetni aranzmani")]
        buket,

        [Display(Name ="Poklon paketi")]
        poklonKutije,

        [Display(Name ="Igracke")]
        igracke,

        [Display(Name ="Ostalo")]
        ostalo
    }
}
