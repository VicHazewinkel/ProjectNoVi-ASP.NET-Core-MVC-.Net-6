using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.ViewModels;

namespace ProjectNoVi_V3.Web.Mapper
{
    public static class ProductMapper
    {
        public static Product Map(ProductViewModel producViewModel)
        {
            Product product = new Product()
            {
                Id = producViewModel.Id,
                Type = producViewModel.Type,
                MerkId = producViewModel.MerkId,
                Collectie = producViewModel.Collectie,
                Kleur = producViewModel.Kleur,
                Materiaal = producViewModel.Materiaal,
                Prijs = producViewModel.Prijs,
                Correctie = producViewModel.Correctie,
            };
            return product;
        }

        public static ProductViewModel Map(Product product, Brand brand)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Type = product.Type,
                MerkId = product.MerkId,
                MerkName = brand.MerkName,
                Collectie = product.Collectie,
                Kleur = product.Kleur,
                Materiaal = product.Materiaal,
                Prijs = product.Prijs,
                Correctie = product.Correctie,
            };

            return productViewModel;
        }
    }
}
