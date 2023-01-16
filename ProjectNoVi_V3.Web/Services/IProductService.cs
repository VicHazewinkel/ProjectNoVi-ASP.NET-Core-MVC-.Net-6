using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;

namespace ProjectNoVi_V3.Web.Services;

public interface IProductService
{
    public List<Product> GetAll();

    /// <summary>
    /// Adds or updates a product in the database
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Upsert(Product product);
}

public class ProductsService : IProductService
{
    private readonly ProjectNoVi_V3_Context _context;

    public ProductsService(ProjectNoVi_V3_Context context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        List<Product> products = new List<Product>();

        var dbProducts = _context.Product.Where(x => x.Id > 0);  // linq

        products.AddRange(dbProducts);
        //products = dbProducts.ToList();

        return products;
    }

    /// <summary>
    /// Adds or updates a product in the database
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Upsert(Product product)
    {
        //toevoegen als ID wordt terug gevonden in de DB

        //var brandName = allBrands.FirstOrDefault(x => x.Id == product.MerkId).MerkName;
        var dbProduct = _context.Product.FirstOrDefault(x => x.Id == product.Id);
        try
        {
            if (dbProduct == null)
            {
                _context.Product.Add(product);
                return _context.SaveChanges();
            }
            else
            {
                _context.Product.Update(dbProduct);
                int updatedEntities = _context.SaveChanges();
                return updatedEntities;
            }
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex);
            return -1;
        }        
    }
}
