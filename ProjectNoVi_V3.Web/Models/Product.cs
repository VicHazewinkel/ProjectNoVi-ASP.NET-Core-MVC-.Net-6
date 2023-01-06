using Microsoft.Build.Framework;
using ProjectNoVi_V3.Enums;
using ProjectNoVi_V3.Models;
using System.ComponentModel.DataAnnotations.Schema;
#nullable enable
namespace ProjectNoVi_V3.Models
{
    public class Product
    {
        public Product()
        {
            Collectie = string.Empty;
            Kleur = string.Empty;
            Materiaal = string.Empty;
        }

        public int Id { get; set; }

        public ProductType Type {get; set; }

        [ForeignKey("Merk")]
        public int MerkId { get; set; }

        public Brand? ProductMerk { get; set; }

        public string Collectie { get; set; }

        public string Kleur { get; set; }

        public string Materiaal { get; set; }

        public int Prijs { get; set; }

        public double Correctie { get; set; } 

    }
}
