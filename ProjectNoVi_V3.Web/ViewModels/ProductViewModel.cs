using ProjectNoVi_V3.Enums;

namespace ProjectNoVi_V3.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public int MerkId { get; set; }
        public string MerkName { get; set; }
        public string Collectie { get; set; }
        public string Kleur { get; set;}
        public string Materiaal { get; set; }
        public int Prijs { get; set; }
        public double Correctie { get; set;}

    }
}
