using System.ComponentModel.DataAnnotations;
namespace BloomBox.Models
{
    public class Proizvod

    {
        [Key]
        private int proizvodId { get; set; }
        private String ime;
        private Double cijena;
        private int uvodznikId;
        private Kategorija kategorija;
        private String slikaURL;
        private String opis;
        private int kolicinaNaStanju;
        private List<String> parametri;

        public String Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        public Double Cijena { get { return cijena; } set { cijena = value; }}
        public int UvodznikId
        {
            get { return uvodznikId; }
            set { uvodznikId = value; }
        }
        public Kategorija Kategorija
        {
            get { return kategorija; } set { kategorija = value; }      
        }
        public String SlikaURL { get { return slikaURL; } set { slikaURL = value; }}
        public String Opis {  get { return opis; } set { opis = value; }}
        public int KolicinaNaStanju { get {return kolicinaNaStanju;} set {kolicinaNaStanju = value; }}
        public List<String> Parametri
        {
            get { return parametri; }
            set{ parametri = value; }
        }
    }
}
