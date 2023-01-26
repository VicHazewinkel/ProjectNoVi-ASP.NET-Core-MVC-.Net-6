using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectNoVi_V3.Models
{

    // _TODO Fix SHopping cart Model 
    // Het ForeignKey-attribuut in Product (naar merk) staat juist.
    // De ForeignKey-attributen in ShoppingCart zijn fout en kunnen niet werken.
    // Volg het model van Merk

    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [Display (Name = "Items")]
        [ValidateNever]
        public Product Product { get; set; }

        [Display(Name = "Qualtity")]
        public int Qualtity { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}
